using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	//Event listenr that replaces the inlined localplayer spawn handling
	//that used to be in the entity spawn tickable.
	[AdditionalRegisterationAs(typeof(ILocalPlayerSpawnedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class BroadcastLocalPlayerSpawnedEventListener : PlayerCreationFinishedEventListener, ILocalPlayerSpawnedEventSubscribable
	{
		private ILocalCharacterDataRepository CharacterDateRepository { get; }

		public event EventHandler<LocalPlayerSpawnedEventArgs> OnLocalPlayerSpawned;

		public BroadcastLocalPlayerSpawnedEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] ILocalCharacterDataRepository characterDateRepository) 
			: base(subscriptionService)
		{
			CharacterDateRepository = characterDateRepository ?? throw new ArgumentNullException(nameof(characterDateRepository));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			//Obviously, we only fire the event if the spawned entity is the local player.
			if (IsSpawningEntityLocalPlayer(args.EntityGuid))
			{
				OnLocalPlayerSpawned?.Invoke(this, new LocalPlayerSpawnedEventArgs(args.EntityGuid));
				
				//Also, once we've encountered the local player spawn we can actually unsubscribe from this event
				Unsubscribe();
			}
		}

		//This was brought over from the TrinityCore times, it used to be more complex to determine.
		private bool IsSpawningEntityLocalPlayer([NotNull] ObjectGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));

			return guid.TypeId == EntityTypeId.TYPEID_PLAYER && CharacterDateRepository.CharacterId == guid.CurrentObjectGuid;
		}
	}
}
