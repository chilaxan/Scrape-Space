FROM python:3.12

COPY ./src/requirements.txt /app/requirements.txt
RUN python3 -m pip install -r /app/requirements.txt

COPY ./src /app

EXPOSE 8080

CMD ["uvicorn", "--host=0.0.0.0", "--port=8080", "app.app:app"]
