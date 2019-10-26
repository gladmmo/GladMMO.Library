using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	//TODO: IGuildStatusChangedEventSubscribable should be an event publisher since many things should handle/deal with it.
	[AdditionalRegisterationAs(typeof(IGuildStatusChangedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RequestLocalPlayerCurrentGuildStatusEventListener : OnRealtimeSocialServiceConnectedEventListener, IGuildStatusChangedEventSubscribable
	{
		private ISocialService SocialService { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public event EventHandler<GuildStatusChangedEventArgs> OnGuildStatusChanged;

		private IEntityGuidMappable<CharacterGuildMembershipStatusResponse> GuildMembershipMappable { get; }

		public RequestLocalPlayerCurrentGuildStatusEventListener(IRealtimeSocialServiceConnectedEventSubscribable subscriptionService,
			[NotNull] ISocialService socialSerive,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IEntityGuidMappable<CharacterGuildMembershipStatusResponse> guildMembershipMappable) 
			: base(subscriptionService)
		{
			SocialService = socialSerive ?? throw new ArgumentNullException(nameof(socialSerive));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			GuildMembershipMappable = guildMembershipMappable ?? throw new ArgumentNullException(nameof(guildMembershipMappable));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				var guildStatus = await SocialService.GetCharacterMembershipGuildStatus(PlayerDetails.LocalPlayerGuid.EntityId);

				if (guildStatus.isSuccessful)
				{
					//Add the guild status change.
					GuildMembershipMappable.AddObject(PlayerDetails.LocalPlayerGuid, guildStatus.Result);

					OnGuildStatusChanged?.Invoke(this, new GuildStatusChangedEventArgs(PlayerDetails.LocalPlayerGuid, guildStatus.Result.GuildId));
				}
			});
		}
	}
}
