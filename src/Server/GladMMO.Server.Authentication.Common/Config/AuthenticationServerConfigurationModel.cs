using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	//TODO: Test, lazy today want to move quickly
	/// <summary>
	/// The configuration options for the authentication server.
	/// </summary>
	[JsonObject]
	public sealed class AuthenticationServerConfigurationModel
	{
		/// <summary>
		/// Should be set to the endpoint/action specified in the HaloLive documentation https://github.com/HaloLive/Documentation
		/// which is /api/auth but it can be configured, however it shouldn't.
		/// </summary>
		[JsonProperty(Required = Required.Default)]
		public string AuthenticationControllerEndpoint { get; set; } //setters required by JSON serializer

		/// <summary>
		/// Should be the path to the X509Certificate2's path that will be used in the signing/issuing of
		/// JWT from the authentication server.
		/// </summary>
		[JsonProperty(Required = Required.Default)]
		public string JwtSigningX509Certificate2Path { get; set; } //setters required by JSON serializer

		/// <summary>
		/// Should be the database string that will allow the authentication service to connect to the table/database
		/// containing the auth tables.
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		public string AuthenticationDatabaseString { get; set; } //setters required by JSON serializer

		/// <summary>
		/// Creates a new configuration for the authentication server.
		/// </summary>
		/// <param name="endpoint">The endpoint/controller/action used for authentication.</param>
		/// <param name="certPath">The path to the JWT signing certificate.</param>
		public AuthenticationServerConfigurationModel(string endpoint, string certPath, string authDbString)
		{
			if(string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(endpoint));
			if(string.IsNullOrWhiteSpace(certPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(certPath));
			if(string.IsNullOrWhiteSpace(authDbString)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(authDbString));

			AuthenticationControllerEndpoint = endpoint;
			JwtSigningX509Certificate2Path = certPath;
			AuthenticationDatabaseString = authDbString;
		}

		/// <summary>
		/// Protected serializer ctor.
		/// </summary>
		public AuthenticationServerConfigurationModel()
		{

		}
	}
}