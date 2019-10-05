using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GladMMO
{
	//From: https://docs.vivox.com/v5/general/unity/5_1_0/Default.htm#AccessTokenDeveloperGuide/TokenFormat/Payload.htm%3FTocPath%3DUnity%7CAccess%2520Token%2520Developer%2520Guide%7CToken%2520Format%7C_____3
	[JsonObject]
	public sealed class VivoxTokenClaims
	{
		/// <summary>
		/// Issuer
		/// </summary>
		[JsonProperty(PropertyName = "iss")]
		public string Issuer { get; private set; }

		/// <summary>
		/// Expiration time
		/// </summary>
		[JsonProperty(PropertyName = "exp")]
		public int ExpiryTime { get; private set; }

		/// <summary>
		/// Vivox action
		/// </summary>
		[JsonProperty(PropertyName = "vxa")]
		public string VivoxAction { get; private set; }

		/// <summary>
		/// Serial number, to guarantee uniqueness within an epoch second
		/// </summary>
		[JsonProperty(PropertyName = "vxi")]
		public int VivoxUniqueIdentifier { get; private set; }

		/// <summary>
		/// From; the SIP URI of the requestor
		/// </summary>
		[JsonProperty(PropertyName = "f")]
		public string OriginatorSIPURI { get; private set; }

		/// <summary>
		/// To; the SIP URI of the channel
		/// </summary>
		[JsonProperty(PropertyName = "t")]
		public string DestinationSIPURI { get; private set; }

		/// <summary>
		/// Subject; used in third-party call control
		/// </summary>
		[JsonProperty(PropertyName = "sub")]
		public string Subject { get; private set; }

		public VivoxTokenClaims(string issuer, int expiryTime, string vivoxAction, int vivoxUniqueIdentifier, string originatorSipuri, string destinationSipuri, string sub)
		{
			Issuer = issuer;
			ExpiryTime = expiryTime;
			VivoxAction = vivoxAction;
			VivoxUniqueIdentifier = vivoxUniqueIdentifier;
			OriginatorSIPURI = originatorSipuri;
			DestinationSIPURI = destinationSipuri;
			Subject = sub;
		}

		[JsonConstructor]
		private VivoxTokenClaims()
		{
			
		}
	}
}
