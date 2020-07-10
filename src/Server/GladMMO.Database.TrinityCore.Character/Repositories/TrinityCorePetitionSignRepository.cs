using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePetitionSignRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, PetitionSign>, ITrinityPetitionSignRepository
	{
		public TrinityCorePetitionSignRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}