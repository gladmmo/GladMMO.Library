call build-norestore-client-release.bat

xcopy build\client  C:\Users\Glader\Documents\Github\GladMMOTrinityCore.Client\Build\Booma.Client_Data\Managed /Y /q /EXCLUDE:BuildExclude.txt