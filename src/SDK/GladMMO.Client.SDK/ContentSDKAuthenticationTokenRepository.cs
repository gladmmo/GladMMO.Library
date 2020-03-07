using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GladMMO
{
	public sealed class ContentSDKAuthenticationTokenRepository : IReadonlyAuthTokenRepository
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string Retrieve()
		{
			return AuthenticationModelSingleton.Instance.AuthenticationToken;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string RetrieveWithType()
		{
			return $"{RetrieveType()} {Retrieve()}";
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string RetrieveType()
		{
			return "Bearer";
		}
	}
}
