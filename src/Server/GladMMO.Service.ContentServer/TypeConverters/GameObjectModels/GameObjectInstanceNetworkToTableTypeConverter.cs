using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladMMO.Database;
using UnityEngine;

namespace GladMMO
{
	
	public sealed class GameObjectInstanceNetworkToTableTypeConverter : ITypeConverterProvider<GameObjectInstanceModel, GameObjectEntryModel>
	{
		public GameObjectEntryModel Convert([NotNull] GameObjectInstanceModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			//TODO: Kinda hacky, we don't have a valid world id.
			//TODO: better handle position crap
			return new GameObjectEntryModel(fromObject.TemplateId, new Vector3<float>(fromObject.InitialPosition.x, fromObject.InitialPosition.y, fromObject.InitialPosition.z), fromObject.YAxisRotation, int.MaxValue);
		}
	}
}
