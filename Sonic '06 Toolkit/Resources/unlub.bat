cd ".\lubs"
for /r %%i in (*.lub) do java -jar ..\unlub.jar "%%~dpni.lub" > "%%~dpni.lua"
xcopy ".\*.lua" "..\luas" /y /i
del ".\*.lua" /q
@ECHO OFF
:delete
del /q /f *.lub
@ECHO OFF
:rename
cd "..\luas"
rename "*.lua" "*.lub"
exit