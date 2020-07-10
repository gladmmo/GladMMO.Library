using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreAuctionbiddersRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, Auctionbidders>, ITrinityAuctionbiddersRepository
	{
		public TrinityCoreAuctionbiddersRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}