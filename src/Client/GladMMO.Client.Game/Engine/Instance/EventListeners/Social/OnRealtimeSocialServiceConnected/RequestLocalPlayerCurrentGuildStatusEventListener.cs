using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RequestLocalPlayerCurrentGuildStatusEventListener : OnRealtimeSocialServiceConnectedEventListener
	{
		private ISocialService SocialService { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private IEntityGuidMappable<CharacterGuildMembershipStatusResponse> GuildMembershipMappable { get; }

		private ILog Logger { get; }

		private IRemoteSocialHubClient SocialClient { get; }

		public RequestLocalPlayerCurrentGuildStatusEventListener(IRealtimeSocialServiceConnectedEventSubscribable subscriptionService,
			[NotNull] ISocialService socialService,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IEntityGuidMappable<CharacterGuildMembershipStatusResponse> guildMembershipMappable,
			[NotNull] ILog logger,
			[NotNull] IRemoteSocialHubClient socialClient) 
			: base(subscriptionService)
		{
			SocialService = socialService ?? throw new ArgumentNullException(nameof(socialService));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			GuildMembershipMappable = guildMembershipMappable ?? throw new ArgumentNullException(nameof(guildMembershipMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			SocialClient = socialClient ?? throw new ArgumentNullException(nameof(socialClient));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				var guildStatus = await SocialService.GetCharacterMembershipGuildStatus(PlayerDetails.LocalPlayerGuid.EntityId);

				if (Logger.IsInfoEnabled)
					Logger.Info($"Local Player GuildStatus: {guildStatus.ResultCode} Id: {guildStatus?.Result?.GuildId}");

				if (guildStatus.isSuccessful)
				{
					//TODO: Don't do it this way. It's useless
					//Add the guild status change.
					GuildMembershipMappable.AddObject(PlayerDetails.LocalPlayerGuid, guildStatus.Result);
				}

				//Kinda hacky but we spoof a guild status change here
				await SocialClient.ReceiveGuildStatusChangedEventAsync(new GuildStatusChangedEventModel(PlayerDetails.LocalPlayerGuid, guildStatus.isSuccessful ? guildStatus.Result.GuildId : 0));
			});
		}
	}
}
