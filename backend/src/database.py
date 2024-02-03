from pymongo import MongoClient
from bson.objectid import ObjectId
from bson.errors import InvalidId

def get_database(name):
    CONNECTION_STRING = "mongodb://mongodb/scape-space"
 
    client = MongoClient(CONNECTION_STRING)
 
    return client['database'][name]