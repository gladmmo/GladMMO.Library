using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class OnCharacterSessionDataChangedInitializeZoneDataEventListener : BaseSingleEventListenerInitializable<ICharacterSessionDataChangedEventSubscribable, CharacterSessionDataChangedEventArgs>
	{
		private IZoneDataRepository ZoneDataRepository { get; }

		public OnCharacterSessionDataChangedInitializeZoneDataEventListener(ICharacterSessionDataChangedEventSubscribable subscriptionService,
			[NotNull] IZoneDataRepository zoneDataRepository) 
			: base(subscriptionService)
		{
			ZoneDataRepository = zoneDataRepository ?? throw new ArgumentNullException(nameof(zoneDataRepository));
		}

		protected override void OnEventFired(object source, CharacterSessionDataChangedEventArgs args)
		{
			//We just need to update the identifier.
			ZoneDataRepository.UpdateZoneId(args.ZoneIdentifier);
		}
	}
}
