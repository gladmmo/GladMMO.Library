using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IAuthenticationResultRecievedEventSubscribable
	{
		event EventHandler<AuthenticationResultEventArgs> OnAuthenticationResultRecieved;
	}

	public sealed class AuthenticationResultEventArgs : EventArgs
	{
		public bool isSuccessful => TokenResult != null && TokenResult.isTokenValid;

		public PlayerAccountJWTModel TokenResult { get; }

		/// <summary>
		/// A failed authentication result.
		/// </summary>
		public AuthenticationResultEventArgs()
		{
			
		}

		/// <inheritdoc />
		public AuthenticationResultEventArgs(PlayerAccountJWTModel tokenResult)
		{
			TokenResult = tokenResult;
		}
	}
}
