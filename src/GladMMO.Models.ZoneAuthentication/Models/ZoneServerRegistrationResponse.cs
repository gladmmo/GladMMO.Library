using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerRegistrationResponse
	{
		/// <summary>
		/// The expected zoneId (after registering online status) which is also
		/// the zone's registered account id.
		/// </summary>
		[JsonProperty]
		public int ZoneId { get; private set; }

		/// <summary>
		/// One-time use ephemeral zone username.
		/// </summary>
		[JsonProperty]
		public string ZoneUserName { get; private set; }

		/// <summary>
		/// One-time use ephemeral zone password.
		/// </summary>
		[JsonProperty]
		public string ZonePassword { get; private set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private ZoneServerRegistrationResponse()
		{
			
		}
	}
}
