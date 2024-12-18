@echo off
REM Define paths
set SRC_DIR="Mod Manager"
set SRC_FILE=GorillaTagMod.cs
set DST_DIR=BepInEx\plugins
set DST_FILE=GorillaTagMod.dll
set TEMP_FILE=GorillaTagModCopy.cs

REM Copy the source file
copy "%SRC_DIR%\%SRC_FILE%" "%TEMP_FILE%"

REM Compile the copied file into a DLL
csc /target:library /out:%DST_FILE% %TEMP_FILE%

REM Move the DLL to the destination directory
move /Y "%DST_FILE%" "%DST_DIR%"

REM Clean up the temporary file
del "%TEMP_FILE%"

echo Build and copy complete!
pause