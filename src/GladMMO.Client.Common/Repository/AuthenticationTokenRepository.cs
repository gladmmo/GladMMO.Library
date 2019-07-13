using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GladMMO
{
	public sealed class AuthenticationTokenRepository : IReadonlyAuthTokenRepository, IAuthTokenRepository
	{
		private static readonly object SyncObj = new object();

		//For ease-of-use between scenes in Unity3D we store this statically.
		//This is very bad, usually, but it's an issue between sharing data between Unity3D.
		/// <summary>
		/// The authentication token.
		/// </summary>
		private static string AuthToken { get; set; } = null;

		/// <inheritdoc />
		public string Retrieve()
		{
			lock(SyncObj)
			{
				if(AuthToken == null)
					throw new InvalidOperationException("Auth token it not initialized.");

				return AuthToken;
			}
		}

		/// <summary>
		/// Default ctor, does not initialize the token.
		/// </summary>
		public AuthenticationTokenRepository()
		{
			//No default value
		}

		/// <summary>
		/// Initializes the <see cref="AuthToken"/> with the provided <see cref="AuthToken"/>
		/// </summary>
		/// <param name="authToken"></param>
		public AuthenticationTokenRepository(string authToken)
		{
			AuthToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
		}

		/// <inheritdoc />
		public string RetrieveWithType()
		{
			//TODO: Make this more efficient
			return $"{RetrieveType()} {AuthToken}";
		}

		/// <inheritdoc />
		public string RetrieveType()
		{
			return "Bearer";
		}

		/// <inheritdoc />
		public void Update(string authToken)
		{
			if(string.IsNullOrEmpty(authToken)) throw new ArgumentException("Value cannot be null or empty.", nameof(authToken));

			lock(SyncObj)
			{
				AuthToken = authToken;
			}
		}
	}
}
