using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ICharacterDefaultActionBarRepository
	{
		Task<CharacterDefaultActionBarEntry[]> RetrieveAllActionsAsync(EntityPlayerClassType classType);
	}
}
