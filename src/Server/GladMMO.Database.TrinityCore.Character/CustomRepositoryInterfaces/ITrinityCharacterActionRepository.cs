using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;

namespace GladMMO
{
	public partial interface ITrinityCharacterActionRepository : IGenericRepositoryCrudable<uint, CharacterAction>
	{
		Task<CharacterAction[]> RetrieveAllForCharacterAsync(int characterId);

		Task<bool> AddAllAsync(CharacterAction[] actions);
	}
}
