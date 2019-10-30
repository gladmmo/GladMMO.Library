# GladMMO.Library

An in development MMORPG framework. Based on ASP Core, SignalR Core, Unity3D, GladNet3, Akka.NET and Protobuf-Net.

Supporting:

- [x] Authentication/Authorization service (OAuth/JWT) 

- [x] Playfab Integration

- [x] Character Services

- [x] Realtime Instance Services

- [x] User-Created-Content UCC (Avatars/Worlds)

- [x] Realtime Social (Guilds, Friends, Text chat)

- [x] Unity3D SDK for UCC

- [x] Vivox Voice Chat

- [x] FinalIK Integration

- [x] VR Support

## Dependencies

**[Microsoft Azure](https://azure.microsoft.com/en-us/) or [Amazon AWS](https://aws.amazon.com/)**

**[FinalIK\*](https://assetstore.unity.com/packages/tools/animation/final-ik-14290)**

**[Azure PlayFab](https://playfab.com/) (backend only)**

**[Unity 2018.4.1f1](https://unity.com/)**

**[Visual Studio >=2017](https://visualstudio.microsoft.com/vs/)**

**ASP Core >=2.0**



\* Not included

## FAQ

**Q. Why does the library use a precompiled version of Microsoft.Azure.ServiceBus?**

Due to issues with Unity3D's netstandard2.0 compatibility with some assemblies it was required, at the time of this writing at least, to fork it to this repository [Glader.Azure.ServiceBus.Unity](https://github.com/HelloKitty/Glader.Azure.ServiceBus.Unity). It removed some of the dependencies and ensure Unity3D compatibility without breaking assembly references.

## Path Variables

**PLAYFAB_KEY:** The secret backend key for PlayFab authorization.

**AZURE_STORAGE_CONNECTIONSTRING:** If using Azure Blob for content delivery this is the variable for the secret connection string for Azure Storage.

**VIVOX_API_KEY:** Vivox authorization service requires a secret key for signing tokens for client authorization.

**AZURE_SERVICE_BUS_KEY:** Global Azure Service Bus secret key that allows the backend to manage, send and listen on Service Queues and Topics.

TODO: AWS

## License

For regular users this repository is licensed under AGPL 3.0. Seperate from the AGPL 3.0, an additional unrestricted, non-exclusive, perpetual, and irrevocable license is also granted to Andrew Blakely for all works in this repository and any dependent GladMMO repository within the GladMMO Github organization.
