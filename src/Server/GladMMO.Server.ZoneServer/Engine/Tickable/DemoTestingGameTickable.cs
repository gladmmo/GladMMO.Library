using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using JetBrains.Annotations;

namespace GladMMO
{
	//To put some demo/testing code into
	[GameInitializableOrdering(1)]
	//[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class DemoTestingGameTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataContainer { get; }

		private float TimePassed = 0.0f;

		/// <inheritdoc />
		public DemoTestingGameTickable([NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataContainer)
		{
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
		}

		/// <inheritdoc />
		public void Tick()
		{
			//Check time passed
			TimePassed += UnityEngine.Time.deltaTime;

			if(TimePassed > 1.0f)
			{
				TimePassed = 0;
			}
			else
				return;

			//We should just decrement every player's health by 10 every second.
			foreach(IEntityDataFieldContainer container in EntityDataContainer.Values)
			{
				container.SetFieldValue((int)EUnitFields.UNIT_FIELD_HEALTH, Math.Max(0, container.GetFieldValue<int>((int)EUnitFields.UNIT_FIELD_HEALTH) - 10));
			}
		}
	}
}
