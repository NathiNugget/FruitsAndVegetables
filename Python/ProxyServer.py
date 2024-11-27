from socket import *
import threading
import json
import requests
import datetime


serverPort = 727

destinationURL = "Placeholder"

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
        if "temperature" not in messageAsDictionary or type(messageAsDictionary["temperature"] != type(float)):
            raise KeyError("temperature was not included in the JSON, or has an invalid value")
        elif "humidity" not in messageAsDictionary or type(messageAsDictionary["humidity"] != type(float)):
            raise KeyError("humidity was not included in the JSON, or has an invalid value")
        response = requests.post(destinationURL, json=messageAsString)
        if response.status_code != 201:
            writeLogEntry(datetime.datetime.now(), "WRONG STATUS CODE", "Status code was " + response.status_code )
            
    except Exception as e:
        # do stuff if there is an error
        writeLogEntry(datetime.datetime.now(), "SENSOR PEER ERROR", repr(e) )
        continue
    