using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ICharacterFriendAddedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RequestFriendsListEventListener : OnLocalPlayerSpawnedEventListener, ICharacterFriendAddedEventSubscribable
	{
		private ISocialService SocialService { get; }

		public event EventHandler<CharacterFriendAddedEventArgs> OnCharacterFriendAdded;

		public RequestFriendsListEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] ISocialService socialService) 
			: base(subscriptionService)
		{
			SocialService = socialService ?? throw new ArgumentNullException(nameof(socialService));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				CharacterFriendListResponseModel friendsResponse = await SocialService.GetCharacterListAsync();

				foreach (int characterId in friendsResponse.CharacterFriendsId)
				{
					NetworkEntityGuid entityGuid = NetworkEntityGuidBuilder.New()
						.WithType(EntityType.Player)
						.WithId(characterId)
						.Build();

					OnCharacterFriendAdded?.Invoke(this, new CharacterFriendAddedEventArgs(entityGuid));
				}
			});
		}
	}
}
