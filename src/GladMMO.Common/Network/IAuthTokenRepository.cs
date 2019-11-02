using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IAuthTokenRepository : IReadonlyAuthTokenRepository
	{
		/// <summary>
		/// Sets a new <see cref="authToken"/>.
		/// Should be non-null.
		/// </summary>
		/// <param name="authToken">The authentication token.</param>
		void Update(string authToken);
	}
}
