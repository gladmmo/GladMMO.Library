using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;

namespace GladMMO
{
	public interface ITrinityCharacterActionBarRepository : IGenericRepositoryCrudable<uint, CharacterAction>
	{
		Task<CharacterAction[]> RetrieveAllForCharacterAsync(int characterId);

		Task<bool> AddAllAsync(CharacterAction[] actions);
	}
}
