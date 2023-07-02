#video_server.py
from simple_websocket_server import WebSocketServer, WebSocket
import base64, cv2
import numpy as np
import warnings
warnings.simplefilter("ignore", DeprecationWarning)


class SimpleEcho(WebSocket):
    def handle(self):
        msg = self.data
        img = cv2.VideoCapture('http://61.43.246.226:1935/rtplive/cctv_193.stream/playlist.m3u8')
        cv2.imshow('image', img)
        cv2.waitKey(1)
        

    def connected(self):
        print(self.address, 'connected')

    def handle_close(self):
        print(self.address, 'closed')


server = WebSocketServer('localhost', 3000, SimpleEcho)
server.serve_forever()