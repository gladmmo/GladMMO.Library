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

# Dependencies

**[Microsoft Azure](https://azure.microsoft.com/en-us/) or [Amazon AWS](https://aws.amazon.com/)**

**[FinalIK\*](https://assetstore.unity.com/packages/tools/animation/final-ik-14290)**

**[Azure PlayFab](https://playfab.com/) (backend only)**

**[Unity 2018.4.1f1](https://unity.com/)**

**[Visual Studio >=2017](https://visualstudio.microsoft.com/vs/)**

**ASP Core >=2.0**



\* Not included

# Builds

**Services:** [![Build status](https://dev.azure.com/vrguardians/VRGuardians.Test/_apis/build/status/VRGuardians%20Test%20-%20CI)](https://dev.azure.com/vrguardians/VRGuardians.Test/_build/latest?definitionId=9)

# Services

All services are built with scale in mind. It's built upon the crossplatform technology ASP.NET Core. This allows GladMMO's services to be high performance and asynchronous stateless HTTP APIs which provide for high scalability.

## [GladMMO.Service.Authentication](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.Authentication)

[GladMMO's Authentication service](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.Authentication) provides authentication and registeration services to player accounts. Supports PlayFab integration opaquely but does **NOT** rely on PlayFab identity or authentication service at all.

The Authentication is based on ASP Core's identity library as well as Openiddict's OAuth/JWT library. It provides a way for player accounts to authorize themselves in the distributed stateless backend using JWT (Web Tokens) to access secure API on other services.

## [GladMMO.Service.ServiceDiscovery](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.ServiceDiscovery)

[GladMMO's Service Discovery service](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.ServiceDiscovery) provides a singular endpoint into the GladMMO backend. It works similar to a DNS service providing routes based on service name. This allows for clients to only ever have to know a single backend endpoint when deployed and can gather other service's endpoints at runtime from this service.

## [GladMMO.Service.ContentServer](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.ContentServer)

[GladMMO's ContentServer service](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.ContentServer) is responsible for handling User Created Content and servicing client's that data at runtime. The GladMMO SDK interacts with this service to allow users to upload content such as: Worlds, Avatars, Creatures and Networked Objects. 

Backed by Azure Storage Blob which it uses in a secure fashion to allow users to upload and update content they've created.

## [GladMMO.Service.Vivox](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.Vivox)

[GladMMO's Vivox service](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.Vivox) is responsible for issuing Vivox authorization tokens to allow users to join channels for text chat and voice chat. It does this in a secure fashion by integrating GladMMO's authoization tokens with Vivox's authorative token generation scheme. It does not actually handle text chat or voice chat, 3rd Vivox servers handle this, but a user cannot interact with those services without authorization from GladMMO.Service.Vivox.

## [GladMMO.Service.NameQuery](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.NameQuery)

[GladMMO's Name Query service](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.NameQuery) is responsible for translating [NetworkEntityGuid's](https://github.com/gladmmo/GladMMO.Library/blob/master/src/GladMMO.Common/Guid/NetworkEntityGuid.cs) into human readable string names. This includes: Players, creatures, gameobjects and more. Acting as a reverse-DNS for Entity's on the platform the service takes globally unique 64bit data encoded identifiers and translates them to non-unique names. This allows for the platform to address entities using the 64bit NetworkEntityGuid and not by string name allowing for high performance and easy addressing for actions. It's the identifier that drives the Entity Data Component's in GladMMO's ECS.

## [GladMMO.Service.Social](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.Social)

[GladMMO's Social service](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.Social) is responsible for the realtime non-chat Social events. This includes guilds, friends, social notifications and eventually groups.

Based on Microsoft's SignalR it utilizes ASP Core and SignalR together to deliver a scalable realtime websockets solutions for modern social features expected in an online game platform. Eventually will support integration into Microsoft's Azure's managed SignalR service.

## [GladMMO.Service.ZoneAuthentication](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.ZoneAuthentication)

[GladMMO Zone Authentication service](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.ZoneAuthentication) is similar in feature set and technology to GladMMO's player authentication service. However, this is an account registeration and authentication service specifically for zone/instance servers. It creates ephemeral one-time accounts for Zone/Instance Servers that are spun up either on the backend or by user's themselves. Supporting the user self-hosting feature of GladMMO securely. It uses linked JWT authorization for zone registeration to allow only authorized players to create zone server accounts.

## [GladMMO.Service.ZoneManager](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.ZoneManager)

[GladMMO's Zone Manager service](https://github.com/gladmmo/GladMMO.Library/tree/master/src/Server/GladMMO.Service.ZoneManager) is responsible for managing zone servers that come online. After zone account creation and authorization with GladMMO.Service.ZoneAuthentication zone servers are able to register themselves on the backend allowing for the ability for users to be routed to them. It's also responsible for handling zone health checks by processing them to ensure zones that are no longer available are cleaned up.

It utilizes Azure Service Bus message queue to process checkins and character data persistence requests in a fault tolerant and highly available manner. Making sure that the distributed nature fo GladMMO's zone/instance server can survive even if parts of the GladMMO backend become unavailable.

# FAQ

**Q. Why does the library use a precompiled version of Microsoft.Azure.ServiceBus?**

Due to issues with Unity3D's netstandard2.0 compatibility with some assemblies it was required, at the time of this writing at least, to fork it to this repository [Glader.Azure.ServiceBus.Unity](https://github.com/HelloKitty/Glader.Azure.ServiceBus.Unity). It removed some of the dependencies and ensure Unity3D compatibility without breaking assembly references.

# Path Variables

**PLAYFAB_KEY:** The secret backend key for PlayFab authorization.

**AZURE_STORAGE_CONNECTIONSTRING:** If using Azure Blob for content delivery this is the variable for the secret connection string for Azure Storage.

**VIVOX_API_KEY:** Vivox authorization service requires a secret key for signing tokens for client authorization.

**AZURE_SERVICE_BUS_KEY:** Global Azure Service Bus secret key that allows the backend to manage, send and listen on Service Queues and Topics.

**AUTHENTICATION_DATABASE_CONNECTION_STRING:** Variable for authentication database connection string. It's used in cloud deployments in lieu of the connection configuration file.

TODO: AWS

# License

For regular users this repository is licensed under AGPL 3.0. Seperate from the AGPL 3.0, an additional unrestricted, non-exclusive, perpetual, and irrevocable license is also granted to Andrew Blakely for all works in this repository and any dependent GladMMO repository within the GladMMO Github organization.
