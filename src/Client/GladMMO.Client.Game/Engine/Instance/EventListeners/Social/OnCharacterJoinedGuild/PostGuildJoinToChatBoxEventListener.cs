using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class PostGuildJoinToChatBoxEventListener : BaseSingleEventListenerInitializable<ICharacterJoinedGuildEventSubscribable, CharacterJoinedGuildEventArgs>
	{
		private IEntityNameQueryable EntityNameQueryable { get; }

		private IChatTextMessageRecievedEventPublisher TextChatPublisher { get; }

		public PostGuildJoinToChatBoxEventListener(ICharacterJoinedGuildEventSubscribable subscriptionService,
			[NotNull] IEntityNameQueryable entityNameQueryable,
			[NotNull] IChatTextMessageRecievedEventPublisher textChatPublisher) 
			: base(subscriptionService)
		{
			EntityNameQueryable = entityNameQueryable ?? throw new ArgumentNullException(nameof(entityNameQueryable));
			TextChatPublisher = textChatPublisher ?? throw new ArgumentNullException(nameof(textChatPublisher));
		}

		protected override void OnEventFired(object source, CharacterJoinedGuildEventArgs args)
		{
			//If it's a hidden join then the client shouldn't write it to the chat box.
			if (args.isHiddenJoin)
				return;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				string inviterName = await EntityNameQueryable.RetrieveAsync(args.JoineeGuid)
					.ConfigureAwait(true);

				TextChatPublisher.PublishEvent(this, new TextChatEventArgs($"[{inviterName}] has joined the guild.", ChatChannelType.System));
			});
		}
	}
}
