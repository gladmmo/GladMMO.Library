using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for service that signs a Vivox claims.
	/// </summary>
	public interface IVivoxTokenSignService
	{
		/// <summary>
		/// Creates a signed token authorizing it as officially
		/// useable.
		/// </summary>
		/// <param name="claims">The token claims</param>
		/// <returns>The signed authorization token in string form.</returns>
		string CreateSignature(VivoxTokenClaims claims);
	}
}
