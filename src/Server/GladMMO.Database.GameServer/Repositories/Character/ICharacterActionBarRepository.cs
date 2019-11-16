using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ICharacterActionBarRepository : IGenericRepositoryCrudable<int, CharacterActionBarEntry>
	{
		Task<CharacterActionBarEntry[]> RetrieveAllForCharacterAsync(int characterId);
	}
}
