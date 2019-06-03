using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// The authentication request model.
	/// </summary>
	[JsonObject]
	public sealed class AuthenticationRequestModel : IUserAuthenticationDetailsContainer
	{
		/// <summary>
		/// Username for the authentication request.
		/// </summary>
		[NotNull]
		[AliasAs("username")]
		[JsonProperty(PropertyName = "username", Required = Required.Always)]
		public string UserName { get; private set; } //setter required by refit

		/// <summary>
		/// Password for the authentication request.
		/// </summary>
		[NotNull]
		[AliasAs("password")]
		[JsonProperty(PropertyName = "password", Required = Required.Always)]
		public string Password { get; private set; } //setter required by refit

		/// <summary>
		/// The OAuth grant type.
		/// </summary>
		[NotNull]
		[AliasAs("grant_type")]
		[JsonProperty(PropertyName = "grant_type", Required = Required.Always)]
		public string GrantType { get; private set; } = "password"; //setter required by refit

		/// <summary>
		/// Unique value used to prevent replay attacks.
		/// </summary>
		[NotNull]
		[AliasAs("nonce")]
		[JsonProperty(PropertyName = "nonce", Required = Required.Default)]
		public string Nonce { get; private set; } = Guid.NewGuid().ToString(); //This isn't technically cryptographically secure, but it's enough.

		/// <summary>
		/// Creates a new Authentication Request Model.
		/// </summary>
		/// <param name="userName">The non-null username.</param>
		/// <param name="password">The non-null password.</param>
		public AuthenticationRequestModel([NotNull] string userName, [NotNull] string password)
		{
			if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
			if(string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

			UserName = userName;
			Password = password;
		}

		/// <summary>
		/// Serializer ctor
		/// </summary>
		private AuthenticationRequestModel()
		{

		}

		/// <inheritdoc />
		public override string ToString()
		{
			//Just return the body that will likely be used for the auth
			return $"grant_type=password&username={UserName}&password={Password}&nonce={Nonce}";
		}
	}
}
