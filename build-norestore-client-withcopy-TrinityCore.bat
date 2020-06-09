call build-norestore-client.bat

xcopy build\client  C:\Users\Glader\Documents\Github\GladMMOTrinityCore.Client\Assets\DLLs /Y /q /EXCLUDE:BuildExclude.txt
xcopy build\client  C:\Users\Glader\Documents\Github\GladMMOTrinityCore.Client\Build\Booma.Client_Data\Managed /Y /q /EXCLUDE:BuildExclude.txt