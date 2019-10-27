using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Fasterflect;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RetrieveGuildListEventListener : OnLocalPlayerGuildStatusChangedEventListener
	{
		private ISocialService SocialService { get; }

		private ILog Logger { get; }

		private ICharacterJoinedGuildEventPublisher GuildJoinEventPublisher { get; }

		public RetrieveGuildListEventListener(IGuildStatusChangedEventSubscribable subscriptionService, IReadonlyLocalPlayerDetails localPlayerDetails,
			[NotNull] ISocialService socialService,
			[NotNull] ILog logger,
			[NotNull] ICharacterJoinedGuildEventPublisher guildJoinEventPublisher) 
			: base(subscriptionService, localPlayerDetails)
		{
			SocialService = socialService ?? throw new ArgumentNullException(nameof(socialService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GuildJoinEventPublisher = guildJoinEventPublisher ?? throw new ArgumentNullException(nameof(guildJoinEventPublisher));
		}

		protected override void OnGuildStatusChanged(GuildStatusChangedEventModel changeArgs)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				var rosterResponseModel = await SocialService.GetGuildListAsync();

				if (!rosterResponseModel.isSuccessful)
				{
					if(Logger.IsWarnEnabled)
						Logger.Warn($"Failed to query guild roster. Reason: {rosterResponseModel.ResultCode}");
					return;
				}

				//Now we can publish the roster.
				foreach (int rosterCharacterId in rosterResponseModel.Result.GuildedCharacterIds)
				{
					NetworkEntityGuid characterGuid = NetworkEntityGuidBuilder.New()
						.WithType(EntityType.Player)
						.WithId(rosterCharacterId)
						.Build();

					//This is a hidden join, or the alerts would be spammed.
					GuildJoinEventPublisher.PublishEvent(this, new CharacterJoinedGuildEventArgs(characterGuid, true));
				}
			});
		}
	}
}
