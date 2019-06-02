call build-norestore-instanceserver.bat

xcopy build\zoneserv  C:\Users\Glader\Documents\Github\GladMMO.InstanceServer\Assets\DLL /Y /q /EXCLUDE:BuildExclude.txt
PAUSE