from socket import *
import threading
import json
import requests
import datetime

headers = {"Content-Type": "application/json"}

serverPort = 727

#TODO: Change placeholder to real destination IP addr
destinationURL = "https://fruitcontainerproduction.azurewebsites.net/api/Readings"

serverName = "0.0.0.0"

serverSocket = socket(AF_INET, SOCK_DGRAM)

serverSocket.bind((serverName, serverPort))

def writeLogEntry(timeStamp, prefix, text):
    with open("./proxylog.txt", "a") as file:
        file.write(f"[{timeStamp.date()} {timeStamp.hour}:{timeStamp.minute}:{timeStamp.second}] {prefix}: {text}\n")



while True:
    try:
        message, _ = serverSocket.recvfrom(15441600)
        messageAsString = message.decode()
        messageAsDictionary = json.loads(messageAsString)
        if "temperature" not in messageAsDictionary or (type(messageAsDictionary["temperature"]) is not float and type(messageAsDictionary["temperature"]) is not int):
            raise KeyError("temperature was not included in the JSON, or has an invalid value")
        elif "humidity" not in messageAsDictionary or (type(messageAsDictionary["humidity"]) is not float and type(messageAsDictionary["humidity"]) is not int):
            raise KeyError("humidity was not included in the JSON, or has an invalid value")
        # TODO: Remove "verify=False" if we can make it work without
        response = requests.post(destinationURL, data=messageAsString, headers=headers, verify=False)
        if response.status_code != 201:
            writeLogEntry(datetime.datetime.now(), "WRONG STATUS CODE", "Status code was " + str(response.status_code) + " RESPONSE TEXT: " + response.text )
            
    except Exception as e:
        # do stuff if there is an error
        writeLogEntry(datetime.datetime.now(), "SENSOR PEER ERROR", repr(e) )
        continue
    