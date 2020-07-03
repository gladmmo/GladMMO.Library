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
	public sealed class CharacterGuildRosterUICreationEventListener : EventQueueBasedTickable<ICharacterJoinedGuildEventSubscribable, CharacterJoinedGuildEventArgs>
	{
		private IUIParentable GuildListWindowRoot { get; }

		private IEntityPrefabFactory PrefabFactory { get; }

		private IEntityNameQueryable NameQueryable { get; }

		public CharacterGuildRosterUICreationEventListener(ICharacterJoinedGuildEventSubscribable subscriptionService, ILog logger,
			[KeyFilter(UnityUIRegisterationKey.GuildList)] [NotNull] IUIParentable guildListWindowRoot,
			[NotNull] IEntityPrefabFactory prefabFactory,
			[NotNull] IEntityNameQueryable nameQueryable) 
			: base(subscriptionService, false, logger) //one at a time to not cause any stutters.
		{
			GuildListWindowRoot = guildListWindowRoot ?? throw new ArgumentNullException(nameof(guildListWindowRoot));
			PrefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
			NameQueryable = nameQueryable ?? throw new ArgumentNullException(nameof(nameQueryable));
		}

		protected override void HandleEvent(CharacterJoinedGuildEventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				string characterName = await NameQueryable.RetrieveAsync(args.JoineeGuid);

				GameObject slotObject = GameObject.Instantiate(PrefabFactory.Create(EntityPrefab.CharacterGuildSlot));
				IUICharacterFriendSlot characterFriendSlot = slotObject.GetComponent<IUICharacterFriendSlot>();

				//We're on the main thread here, we can create the tab.
				GuildListWindowRoot.Parent(slotObject);

				//TODO: Query for name.
				characterFriendSlot.Text = characterName;
				characterFriendSlot.LocationText.Text = GladMMOCommonConstants.DEFAULT_UNKNOWN_ENTITY_NAME_STRING;
			});
		}
	}
}
