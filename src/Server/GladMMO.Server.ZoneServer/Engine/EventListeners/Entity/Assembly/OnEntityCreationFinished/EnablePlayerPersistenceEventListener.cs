using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class EnablePlayerPersistenceEventListener : PlayerCreationFinishedEventListener
	{
		private IEntityGuidMappable<EntitySaveableConfiguration> PlayerPersistenceConfigMappable { get; }

		public EnablePlayerPersistenceEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<EntitySaveableConfiguration> playerPersistenceConfigMappable) 
			: base(subscriptionService)
		{
			PlayerPersistenceConfigMappable = playerPersistenceConfigMappable ?? throw new ArgumentNullException(nameof(playerPersistenceConfigMappable));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			//Right now it's just default.
			PlayerPersistenceConfigMappable.AddObject(args.EntityGuid, new EntitySaveableConfiguration());
		}
	}
}
