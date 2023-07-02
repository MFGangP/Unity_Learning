import cv2
import numpy as np

cap = cv2.VideoCapture("http://61.43.246.226:1935/rtplive/cctv_193.stream/playlist.m3u8")

while True:
    ret, img = cap.read()
    if ret == True:
        cv2.imshow('video output', img)
        k = cv2.waitKey(30)& 0xff
    if k == 27:
        break
cap.release()
cv2.destroyAllWindows()  