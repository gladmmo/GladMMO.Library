call build-norestore-client.bat

xcopy build\client  C:\Users\Glader\Documents\Github\GladMMOTrinityCore.Client\Assets\DLLs /Y /q /EXCLUDE:BuildExclude.txt
xcopy build\client  C:\Users\Glader\Documents\Github\GladMMO.GaiaOnline.Client\Assets\DLLs /Y /q /EXCLUDE:BuildExclude.txt
xcopy build\client  "C:\Users\Glader\Documents\Unity Projects\GladMMO.SAOGG.Client\Assets\DLLs" /Y /q /EXCLUDE:BuildExclude.txt