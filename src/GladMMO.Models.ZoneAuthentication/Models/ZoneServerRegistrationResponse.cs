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

		public ZoneServerRegistrationResponse(int zoneId, string zoneUserName, string zonePassword)
		{
			if (zoneId <= 0) throw new ArgumentOutOfRangeException(nameof(zoneId));
			if (string.IsNullOrWhiteSpace(zoneUserName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(zoneUserName));
			if (string.IsNullOrWhiteSpace(zonePassword)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(zonePassword));

			ZoneId = zoneId;
			ZoneUserName = zoneUserName;
			ZonePassword = zonePassword;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private ZoneServerRegistrationResponse()
		{
			
		}
	}
}
