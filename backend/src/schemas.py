from pydantic import BaseModel

class UserBase(BaseModel):
    username: str

class UserCreate(UserBase):
    password: str

class UserLogin(UserBase):
    password: str

class UserNoUpgrades(UserBase):
    _id: int
    score: int

    class Config:
        from_attributes = True

class User(UserNoUpgrades):
    upgrades: list[str]

class Board(BaseModel):
    board: list[UserNoUpgrades]