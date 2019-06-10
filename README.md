# GladMMO.Library

An in development MMORPG framework. Based on ASP Core, SignalR Core, Unity3D, GladNet3 and Protobuf-net.

Supporting:

- [x] Authentication/Authorization service (OAuth/JWT) 

- [x] Playfab Integration

- [x] Character Services

- [x] Realtime Instance Services

- [x] User-Created-Content UCC (Avatars/Worlds)

- [x] Realtime Social (Guilds, Friends, Text chat)

- [x] Unity3D SDK for UCC

- [ ] Vivox Voice Chat

- [ ] FinalIK Integration

- [ ] VR Support

## Dependencies

**[Microsoft Azure](https://azure.microsoft.com/en-us/) or [Amazon AWS](https://aws.amazon.com/)**

**[FinalIK\*](https://assetstore.unity.com/packages/tools/animation/final-ik-14290)**

**[Azure PlayFab](https://playfab.com/) (backend only)**

**[Unity 2018.4.1f1](https://unity.com/)**

**[Visual Studio >=2017](https://visualstudio.microsoft.com/vs/)**

**ASP Core >=2.0**



\* Not included

## Path Variables

**PLAYFAB_KEY:** The secret backend key for PlayFab authorization.

**AZURE_STORAGE_CONNECTIONSTRING:** If using Azure Blob for content delivery this is the variable for the secret connection string for Azure Storage.

TODO: AWS

## License

For regular users this repository is licensed under AGPL 3.0. Seperate from the AGPL 3.0, an additional unrestricted, non-exclusive, perpetual, and irrevocable license is also granted to Andrew Blakely for all works in this repository and any dependent repository.
