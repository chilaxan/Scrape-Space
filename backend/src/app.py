from typing import Annotated
from fastapi import Depends, FastAPI, HTTPException, Response, Cookie
from fastapi.responses import PlainTextResponse
from pymongo import DESCENDING

from .database import get_database, ObjectId
from . import utils
from . import schemas

app = FastAPI(root_path='/api')

def authentication(auth: Annotated[str | None, Cookie()] = None):
    if auth:
        data = utils.decode(auth)
        users = get_database('users')
        if data:
            return users.find_one({'_id': ObjectId(data.get('id'))})
    raise HTTPException(status_code=400, detail="Failed To authenticate")

@app.post("/login", response_model=schemas.User, tags=['auth'])
def login(logging_in_user: schemas.UserLogin, response: Response):
    users = get_database('users')
    user = users.find_one({'username': logging_in_user.username})
    if user and utils.check_password(logging_in_user.password, user['hashed_password']):
        response.set_cookie("auth", utils.sign({'id': str(user['_id'])}))
        return user
    raise HTTPException(status_code=400, detail="Failed To login")

@app.post("/register", response_model=schemas.User, tags=['auth'])
def create_user(user: schemas.UserCreate, response: Response):
    users = get_database('users')
    user.username = user.username.strip()
    db_user = users.find_one({'username': user.username})
    if db_user:
        raise HTTPException(status_code=400, detail="Username already registered")
    if not utils.check_username(user.username):
        raise HTTPException(status_code=400, detail="Invalid Username")
    insert_id = users.insert_one(user := {
        'username': user.username,
        'score': 0,
        'upgrades': [],
        'hashed_password': utils.get_hashed_password(user.password)
    }).inserted_id
    response.set_cookie("auth", utils.sign({"id": str(insert_id)}))
    return users.find_one({'_id': insert_id})

@app.post('/logout', tags=['auth'])
def logout(response: Response) -> None:
    response.delete_cookie("auth")

@app.get('/user', response_model=schemas.User, tags=['user'])
async def get_user(user = Depends(authentication)):
    return user

@app.post('/upgrade', response_model=schemas.User, tags=['user'])
async def upgrade(upgrade_id: str, user = Depends(authentication)):
    if upgrade_id not in user['upgrades']:
        users = get_database('users')
        users.update_one({'_id': user['_id']}, {"$push": {'upgrades': upgrade_id}})
    return users.find_one({'_id': user['_id']})

@app.post('/downgrade', response_model=schemas.User, tags=['user'])
async def downgrade(downgrade_id: str, user = Depends(authentication)):
    if downgrade_id in user['upgrades']:
        users = get_database('users')
        users.update_one({'_id': user['_id']}, {"$pull": {'upgrades': downgrade_id}})
    return users.find_one({'_id': user['_id']})

@app.get('/leaderboard', response_class=PlainTextResponse, tags=['leaderboard'])
async def leaderboard(skip: int = 0, limit: int = 10):
    users = get_database('users')
    return '\n'.join(f'{user["username"]}: {user["score"]}' for user in users.find() \
                    .sort('score', DESCENDING) \
                    .skip(skip) \
                    .limit(limit))

@app.post('/delta', tags=['user'])
async def delta(amount: int, user = Depends(authentication)):
    users = get_database('users')
    users.update_one({'_id': user['_id']}, {'$inc': {'score': amount}})

if __name__ == '__main__':
    app.run('0.0.0.0', port=8080, debug=True)
