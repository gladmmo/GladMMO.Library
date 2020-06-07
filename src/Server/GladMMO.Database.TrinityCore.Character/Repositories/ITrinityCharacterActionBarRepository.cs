using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ITrinityCharacterActionBarRepository : IGenericRepositoryCrudable<uint, CharacterAction>
	{
		Task<CharacterAction[]> RetrieveAllForCharacterAsync(int characterId);
	}
}
