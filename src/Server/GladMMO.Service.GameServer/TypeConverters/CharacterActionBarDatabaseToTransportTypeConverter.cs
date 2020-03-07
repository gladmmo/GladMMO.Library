using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class CharacterActionBarDatabaseToTransportTypeConverter : ITypeConverterProvider<ICharacterActionBarEntry, CharacterActionBarInstanceModel>
	{
		public CharacterActionBarInstanceModel Convert([NotNull] ICharacterActionBarEntry fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new CharacterActionBarInstanceModel(fromObject.BarIndex, fromObject.ActionType, fromObject.ActionId);
		}
	}
}
