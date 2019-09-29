using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class GameObjectTemplateTableToNetworkTypeConverter : ITypeConverterProvider<GameObjectTemplateEntryModel, GameObjectTemplateModel>
	{
		public GameObjectTemplateModel Convert([NotNull] GameObjectTemplateEntryModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new GameObjectTemplateModel(fromObject.GameObjectTemplateId, fromObject.ModelId, fromObject.GameObjectName, fromObject.Type);
		}
	}
}
