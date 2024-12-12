import base64
from picamzero import Camera


path = "./newest_image.png"

def TakeImage():
    cam = Camera()
    cam.still_size = (4608,2592)
    cam.take_photo(path)
