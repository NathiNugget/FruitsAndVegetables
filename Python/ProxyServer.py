from socket import *
import threading
import json
import requests
import datetime
import paramiko
import os
from pathlib import Path
home = Path.home()


headers = {"Content-Type": "application/json"}

serverPort = 727

#TODO: Change placeholder to real destination IP addr
destinationReadingURL = "https://fruitcontainerproduction.azurewebsites.net/api/Readings"
destinationImageURL = "https://fruitcontainerproduction.azurewebsites.net/api/Images"

serverName = "0.0.0.0"

serverSocket = socket(AF_INET, SOCK_DGRAM)

serverSocket.bind((serverName, serverPort))

def writeLogEntry(timeStamp, prefix, text):
    with open("./proxylog.txt", "a") as file:
        file.write(f"[{timeStamp.date()} {timeStamp.hour}:{timeStamp.minute}:{timeStamp.second}] {prefix}: {text}\n")



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
        
        
        client = paramiko.SSHClient()
        #client.set_missing_host_key_policy(paramiko.AutoAddPolicy())

        print(os.listdir(home/".ssh"))
        client.load_host_keys(home/".ssh"/"known_hosts")
        print(client.get_host_keys().keys())
        client.connect(f'{returnAddress[0]}', port=22, username='pi', password='raspberry')
        print(client.get_host_keys())
        SFTP = client.open_sftp()
        SFTP.get("/home/pi/Nathaniel/newest_image.jpg", "newest_image.jpg")
        SFTP.close()
        client.close()    
            





        if response.status_code != 201:
            writeLogEntry(datetime.datetime.now(), "WRONG STATUS CODE", "Status code was " + str(response.status_code) + " RESPONSE TEXT: " + response.text )

    except Exception as e:
        # do stuff if there is an error
        writeLogEntry(datetime.datetime.now(), "SENSOR PEER ERROR", repr(e) )
        print("Something went wrong, Nath!")
        continue
    