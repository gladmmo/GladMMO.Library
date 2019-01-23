﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet;
using Microsoft.AspNetCore.SignalR;

namespace Guardians
{
	public interface IHubConnectionMessageContext<TRemoteClientHubInterfaceType> : IHubConnectionMessageContext
	{
		IHubCallerClients<TRemoteClientHubInterfaceType> Clients { get; }
	}

	public interface IHubConnectionMessageContext
	{
		IGroupManager Groups { get; }

		HubCallerContext HubConntext { get; }

		//This is from GladNet3
		IConnectionService ConnectionService { get; }
	}
}
