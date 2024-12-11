import base64
from picamzero import Camera


path = "./newest_image.png"

def TakeImage():
    cam = Camera()
    cam.still_size = (4608,2592)
    cam.take_photo(path)

# def GetDataToSend(): 
#     with open(path, "rb") as image_file:
#         data = base64.b64encode(image_file.read())
#         dataDict = {"bytes": str(data)}
#         return dataDict

# def SnapAndSend():
#     TakeImage()
#     bytes = GetDataToSend()
#     return bytes


# im = Image.open(BytesIO(base64.b64decode(data)))
# im.save('after.png', 'PNG')


