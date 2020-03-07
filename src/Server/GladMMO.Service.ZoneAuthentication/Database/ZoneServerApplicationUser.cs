using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GladMMO
{
	public class ZoneServerApplicationUser : IdentityUser<int>
	{
		//Unlike normal guardians users we DO have additional data.
		[Required]
		public int AccountId { get; private set; }

		public ZoneServerApplicationUser(int accountId) 
			: base()
		{
			if (accountId <= 0) throw new ArgumentOutOfRangeException(nameof(accountId));

			AccountId = accountId;
		}

		public ZoneServerApplicationUser(int accountId, string userName)
			: base(userName)
		{
			if(accountId <= 0) throw new ArgumentOutOfRangeException(nameof(accountId));

			AccountId = accountId;
		}

		/// <summary>
		/// EF Ctor.
		/// </summary>
		private ZoneServerApplicationUser()
		{
			
		}
	}
}
