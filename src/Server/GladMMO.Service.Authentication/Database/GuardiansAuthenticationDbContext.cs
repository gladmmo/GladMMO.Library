using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	//TODO: Fix doc
	//TODO: Convert from HaloLive to our own model if needed
	/// <summary>
	/// See Documentation for details: https://github.com/openiddict/openiddict-core
	/// </summary>
	public class GuardiansAuthenticationDbContext : IdentityDbContext<GuardiansApplicationUser, GuardiansApplicationRole, int>
	{
		public GuardiansAuthenticationDbContext(DbContextOptions<GuardiansAuthenticationDbContext> options)
			: base(options)
		{

		}
	}
}
