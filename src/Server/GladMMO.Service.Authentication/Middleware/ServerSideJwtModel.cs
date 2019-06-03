using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ServerSideJwtModel
	{
		//Only used on the server.
		[CanBeNull]
		[JsonProperty(PropertyName = "id_token", Required = Required.Default)] //optional because could be a valid token
		public string OpenId { get; private set; }

		/// <summary>
		/// JWT access token if authentication was successful.
		/// </summary>
		[CanBeNull]
		[JsonProperty(PropertyName = "access_token", Required = Required.Default)] //optional because could be an error
		public string AccessToken { get; private set; } //WARNING: Don't make these readonly. It breakes for some reason.

		public ServerSideJwtModel([NotNull] string openId)
		{
			if (string.IsNullOrWhiteSpace(openId))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(openId));

			OpenId = openId;
		}

		[JsonConstructor]
		public ServerSideJwtModel()
			: base()
		{
			
		}
	}
}
