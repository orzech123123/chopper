@echo off
adb kill-server
adb tcpip 5555
adb connect %1
adb logcat | findstr %2

:: example usage:
:: start-adb.cmd 192.168.1.101 Healthy