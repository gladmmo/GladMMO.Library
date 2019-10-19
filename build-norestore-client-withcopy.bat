call build-norestore-client.bat

xcopy build\client  C:\Users\Glader\Documents\GitHub\GladMMO.Client\Assets\DLLs /Y /q /EXCLUDE:BuildExclude.txt
xcopy build\client  C:\Users\Glader\Documents\GitHub\VRGuardians.Client\Assets\DLLs /Y /q /EXCLUDE:BuildExclude.txt
PAUSE