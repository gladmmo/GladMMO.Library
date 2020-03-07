using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	//Different from authserver, it uses ZoneServerApplicationUser.
	/// <summary>
	/// See Documentation for details: https://github.com/openiddict/openiddict-core
	/// </summary>
	public class GuardiansZoneAuthenticationDbContext : IdentityDbContext<ZoneServerApplicationUser, GuardiansApplicationRole, int>
	{
		public GuardiansZoneAuthenticationDbContext(DbContextOptions<GuardiansZoneAuthenticationDbContext> options)
			: base(options)
		{

		}
	}
}
