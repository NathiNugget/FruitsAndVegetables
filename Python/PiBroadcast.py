from sense_hat import SenseHat
from socket import *
import time
import json
import picamera
import datetime
import io
import base64


serverName = '<broadcast>'
serverPort = 727

clientSocket = socket(AF_INET, SOCK_DGRAM)
clientSocket.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)

s = SenseHat()
interval = 3600

def writeLogEntry(timeStamp, prefix, text):
    with open("./broadcastlog.txt", "a") as file:
        file.write(f"[{timeStamp.date()} {timeStamp.hour}:{timeStamp.minute}:{timeStamp.second}] {prefix}: {text}\n")

while True: 
  try:
    temp = round(s.temp, 1)
    humidity = round(s.humidity,1)
    image = io.BytesIO()
    # maybe harcode a specific resolution of the img
    picamera.capture(image, "png")
    imageBase64 = base64.b64encode(image)
    reading = {'temperature': temp,'humidity': humidity, "image": imageBase64}
    jsonpack = json.dumps(reading)
    clientSocket.sendto(jsonpack.encode(), (serverName, serverPort))
    time.sleep(interval)
  except Exception as e:
     writeLogEntry(datetime.datetime.now(), "SERVER ERROR", repr(e) )
     continue
