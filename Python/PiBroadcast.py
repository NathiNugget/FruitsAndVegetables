from sense_hat import SenseHat
from socket import *
from ReadBytes import TakeImage
import time
import json

serverName = '<broadcast>'
serverPort = 727

clientSocket = socket(AF_INET, SOCK_DGRAM)
clientSocket.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)

s = SenseHat()
interval = 3600
mini_interval = 2

def TakeMeasurementAndSendData():
  TakeImage()
  temp = round(s.temp, 1)
  humidity = round(s.humidity,1)
  reading = {'temperature': temp,'humidity': humidity}
  jsonpack = json.dumps(reading)
  clientSocket.sendto(jsonpack.encode(), (serverName, serverPort))

while True: 
  TakeMeasurementAndSendData()
  time.sleep(interval)
