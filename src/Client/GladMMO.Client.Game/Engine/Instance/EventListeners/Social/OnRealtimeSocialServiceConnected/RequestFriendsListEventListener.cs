using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RequestFriendsListEventListener : OnRealtimeSocialServiceConnectedEventListener
	{
		private ISocialService SocialService { get; }

		private ICharacterFriendAddedEventPublisher FriendAddedPublisher { get; }

		public RequestFriendsListEventListener(IRealtimeSocialServiceConnectedEventSubscribable subscriptionService,
			[NotNull] ISocialService socialService,
			[NotNull] ICharacterFriendAddedEventPublisher friendAddedPublisher) 
			: base(subscriptionService)
		{
			SocialService = socialService ?? throw new ArgumentNullException(nameof(socialService));
			FriendAddedPublisher = friendAddedPublisher ?? throw new ArgumentNullException(nameof(friendAddedPublisher));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				CharacterFriendListResponseModel friendsResponse = await SocialService.GetCharacterListAsync();

				foreach(int characterId in friendsResponse.CharacterFriendsId)
				{
					NetworkEntityGuid entityGuid = NetworkEntityGuidBuilder.New()
						.WithType(EntityType.Player)
						.WithId(characterId)
						.Build();

					FriendAddedPublisher.PublishEvent(this, new CharacterFriendAddedEventArgs(entityGuid));
				}
			});
		}
	}
}
