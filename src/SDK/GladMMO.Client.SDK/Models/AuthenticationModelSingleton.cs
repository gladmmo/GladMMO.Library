using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class AuthenticationModelSingleton
	{
		public static AuthenticationModelSingleton Instance { get; }

		public bool isAuthenticated { get; private set; } = false;

		public string AuthenticationToken { get; private set; } = null;

		static AuthenticationModelSingleton()
		{
			Instance = new AuthenticationModelSingleton();
		}

		//Hide ctor
		private AuthenticationModelSingleton()
		{
			
		}

		public void SetAuthenticationState(bool state)
		{
			isAuthenticated = state;

			//Clear token data when authentication state changes.
			if (!isAuthenticated)
				AuthenticationToken = null;
		}

		public void SetAuthenticationToken([NotNull] string token)
		{
			if(string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(token));

			AuthenticationToken = token;
		}
	}
}
