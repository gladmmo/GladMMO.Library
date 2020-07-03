using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CharacterFriendUICreationEventListener : EventQueueBasedTickable<ICharacterFriendAddedEventSubscribable, CharacterFriendAddedEventArgs>
	{
		private IUIParentable FriendWindowRoot { get; }

		private IEntityPrefabFactory PrefabFactory { get; }

		private IEntityNameQueryable NameQueryable { get; }

		public CharacterFriendUICreationEventListener(ICharacterFriendAddedEventSubscribable subscriptionService, 
			ILog logger, 
			[KeyFilter(UnityUIRegisterationKey.FriendsWindow)] [NotNull] IUIParentable friendWindowRoot,
			[NotNull] IEntityPrefabFactory prefabFactory,
			[NotNull] IEntityNameQueryable nameQueryable) 
			: base(subscriptionService, false, logger) //we stagger friends out 1 per frame.
		{
			FriendWindowRoot = friendWindowRoot ?? throw new ArgumentNullException(nameof(friendWindowRoot));
			PrefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
			NameQueryable = nameQueryable ?? throw new ArgumentNullException(nameof(nameQueryable));
		}

		protected override void HandleEvent(CharacterFriendAddedEventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				string characterName = await NameQueryable.RetrieveAsync(args.FriendGuid);

				GameObject slotObject = GameObject.Instantiate(PrefabFactory.Create(EntityPrefab.CharacterFriendSlot));
				IUICharacterFriendSlot characterFriendSlot = slotObject.GetComponent<IUICharacterFriendSlot>();

				//We're on the main thread here, we can create the tab.
				FriendWindowRoot.Parent(slotObject);

				//TODO: Query for name.
				characterFriendSlot.Text = characterName;
				characterFriendSlot.LocationText.Text = GladMMOCommonConstants.DEFAULT_UNKNOWN_ENTITY_NAME_STRING;
			});
		}
	}
}
