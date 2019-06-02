using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerRegisterationRequest
	{
		/// <summary>
		/// The endpoint the ZoneServer could be connected to.
		/// </summary>
		[JsonProperty]
		public ResolvedEndpoint ZoneServerEndpoint { get; private set; }
		
		//TODO: We might want to provide details about the type of instance.

		/// <inheritdoc />
		public ZoneServerRegisterationRequest([NotNull] ResolvedEndpoint zoneServerEndpoint)
		{
			ZoneServerEndpoint = zoneServerEndpoint ?? throw new ArgumentNullException(nameof(zoneServerEndpoint));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private ZoneServerRegisterationRequest()
		{
			
		}
	}
}
