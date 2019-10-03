using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//Basically a stub for where/how to implement player level initialization.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializePlayerLevelEventListener : PlayerCreationFinishedEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		public InitializePlayerLevelEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable) 
			: base(subscriptionService)
		{
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			//TODO: Demo code.
			IEntityDataFieldContainer entityDataFieldContainer = EntityDataMappable.RetrieveEntity(args.EntityGuid);

			entityDataFieldContainer.SetFieldValue((int)EUnitFields.UNIT_FIELD_LEVEL, 1);
		}
	}
}
