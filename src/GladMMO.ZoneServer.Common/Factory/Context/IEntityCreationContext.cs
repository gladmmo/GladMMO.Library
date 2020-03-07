using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IEntityCreationContext : IEntityGuidContainer
	{
		Vector3 InitialPosition { get; }

		float Orientation { get; }

		EntityPrefab PrefabType { get; }
	}
}