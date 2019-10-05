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
		[JsonProperty(PropertyName = "iss", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Issuer { get; private set; }

		/// <summary>
		/// Expiration time
		/// </summary>
		[JsonProperty(PropertyName = "exp", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int ExpiryTime { get; private set; }

		/// <summary>
		/// Vivox action
		/// </summary>
		[JsonProperty(PropertyName = "vxa", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string VivoxAction { get; private set; }

		/// <summary>
		/// Serial number, to guarantee uniqueness within an epoch second
		/// </summary>
		[JsonProperty(PropertyName = "vxi", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int VivoxUniqueIdentifier { get; private set; }

		/// <summary>
		/// From; the SIP URI of the requestor
		/// </summary>
		[JsonProperty(PropertyName = "f", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string OriginatorSIPURI { get; private set; }

		/// <summary>
		/// To; the SIP URI of the channel
		/// </summary>
		[JsonProperty(PropertyName = "t", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string DestinationSIPURI { get; private set; }

		/// <summary>
		/// Subject; used in third-party call control
		/// </summary>
		[JsonProperty(PropertyName = "sub", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Subject { get; private set; }

		public VivoxTokenClaims(string issuer, int expiryTime, string vivoxAction, int vivoxUniqueIdentifier, string originatorSipuri, string destinationSipuri, string sub)
		{
			//Some of these are optional parameters so we shouldn't check null.
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
