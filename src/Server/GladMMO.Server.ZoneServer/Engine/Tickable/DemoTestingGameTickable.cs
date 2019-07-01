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

		private IReadonlyKnownEntitySet KnownEntities { get; }

		/// <inheritdoc />
		public DemoTestingGameTickable([NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataContainer,
			[NotNull] IReadonlyKnownEntitySet knownEntities)
		{
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
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
			foreach(var component in EntityDataContainer.Enumerate(KnownEntities))
			{
				component.SetFieldValue((int)EUnitFields.UNIT_FIELD_HEALTH, Math.Max(0, component.GetFieldValue<int>((int)EUnitFields.UNIT_FIELD_HEALTH) - 10));
			}
		}
	}
}
