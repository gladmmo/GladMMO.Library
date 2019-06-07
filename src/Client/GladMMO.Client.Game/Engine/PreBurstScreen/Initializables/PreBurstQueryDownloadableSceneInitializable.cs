using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// The concept of this initializable is that we wait until character session data
	/// has changed and then download the upcoming scene. Maybe it's a Lobby, maybe it's a Forest.
	/// Either way, we want to download it.
	/// </summary>
	public sealed class PreBurstQueryDownloadableSceneInitializable : ThreadUnSafeBaseSingleEventListenerInitializable<ICharacterSessionDataChangedEventSubscribable, CharacterSessionDataChangedEventArgs>
	{
		//private IZoneServerService ZoneDataService { get; }

		//private IContentServerServiceClient ContentService { get; }

		public PreBurstQueryDownloadableSceneInitializable(ICharacterSessionDataChangedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected override void OnThreadUnSafeEventFired(object source, CharacterSessionDataChangedEventArgs args)
		{
			/*//TODO: Handle failure
			ProjectVersionStage.AssertAlpha();
			//TODO: Handle throwing/error
			//We need to know the world the zone is it, so we can request a download URL for it.
			long worldId = await ZoneDataService.GetZoneWorld(characterSessionData.ZoneId)
				.ConfigureAwait(false);

			//With the worldid we can get the download URL.
			ContentDownloadURLResponse urlDownloadResponse = await ContentService.RequestWorldDownloadUrl(worldId, AuthTokenRepo.RetrieveWithType())
				.ConfigureAwait(false);*/
		}
	}
}
