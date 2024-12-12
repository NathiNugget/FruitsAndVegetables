from socket import *
import json
import requests
from datetime import datetime
import paramiko
import base64
from pathlib import Path
home = Path.home()


headers = {"Content-Type": "application/json"}

serverPort = 727
image_name = "newest_image.jpg"

#TODO: Change placeholder to real destination IP addr
destinationReadingURL = "https://fruitcontainerproduction.azurewebsites.net/api/Readings"
destinationImageURL = "https://fruitcontainertest.azurewebsites.net/api/Images"

serverName = "0.0.0.0"

serverSocket = socket(AF_INET, SOCK_DGRAM)

serverSocket.bind((serverName, serverPort))

def writeLogEntry(timeStamp, prefix, text):
    with open("./proxylog.txt", "a") as file:
        file.write(f"[{timeStamp.date()} {timeStamp.hour}:{timeStamp.minute}:{timeStamp.second}] {prefix}: {text}\n")

def GetDataToSend(image_filename: str) -> dict: 
            with open(image_filename, "rb") as image_file:
                data = base64.b64encode(image_file.read())
                dataDict = {"bytes": str(data)}
                return dataDict

while True:
    try:
        message, returnAddress = serverSocket.recvfrom(15441600)
        messageAsString = message.decode()
        messageAsDictionary = json.loads(messageAsString)
        if "temperature" not in messageAsDictionary or (type(messageAsDictionary["temperature"]) is not float and type(messageAsDictionary["temperature"]) is not int):
            raise KeyError("temperature was not included in the JSON, or has an invalid value")
        elif "humidity" not in messageAsDictionary or (type(messageAsDictionary["humidity"]) is not float and type(messageAsDictionary["humidity"]) is not int):
            raise KeyError("humidity was not included in the JSON, or has an invalid value")


        

        response = requests.post(destinationReadingURL, data=messageAsString, headers=headers)
        if response.status_code != 201:
            writeLogEntry(datetime.now(), "WRONG STATUS CODE", "Status code was " + str(response.status_code) + " RESPONSE TEXT: " + response.text )
        
        client = paramiko.SSHClient()
        client.load_host_keys(home/".ssh"/"known_hosts")
        client.connect(f'{returnAddress[0]}', port=22, username='pi', password='raspberry')
        SFTP = client.open_sftp()
        SFTP.get("/home/pi/Nathaniel/"+image_name, image_name)
        SFTP.close()
        client.close()

        dataDict = GetDataToSend(image_name)
        response = requests.post(url=destinationImageURL, json=dataDict)
        if response.status_code not in (200, 201, 204):
             writeLogEntry(datetime.now(), "Sending image data went wrong", f"Status code: {response.status_code}")
 

    except Exception as e:  
        writeLogEntry(datetime.now(), "SENSOR PEER ERROR", repr(e) )
        continue
    