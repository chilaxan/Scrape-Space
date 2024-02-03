from fastapi import FastAPI

app = FastAPI(root_path='/api')

@app.get('/')
async def root():
    return 'Base Api'

if __name__ == '__main__':
    app.run('0.0.0.0', port=8080, debug=True)
