using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GladMMO
{
	/// <summary>
	/// HaloLive OpenIddict app role.
	/// See Documentation for details: https://github.com/openiddict/openiddict-core
	/// </summary>
	public class GuardiansApplicationRole : IdentityRole { } //we don't need any additional data; we rely directly on identity
}
