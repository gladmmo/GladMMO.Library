using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Glader;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IChatMessageBoxReciever))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DefaultSingleTabeChatMessageBoxReciever : IChatMessageBoxReciever, IGameInitializable
	{
		private IEntityPrefabFactory PrefabFactory { get; }

		private IUIParentable ChatWindow { get; }

		public DefaultSingleTabeChatMessageBoxReciever([NotNull] IEntityPrefabFactory prefabFactory,
			[KeyFilter(UnityUIRegisterationKey.TextChatParentWindow)] IUIParentable chatWindow)
		{
			PrefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
			ChatWindow = chatWindow ?? throw new ArgumentNullException(nameof(chatWindow));
		}

		public void ReceiveChatMessage(int tabId, [NotNull] string text)
		{
			if (text == null) throw new ArgumentNullException(nameof(text));
			if (tabId <= 0) throw new ArgumentOutOfRangeException(nameof(tabId));

			GameObject textObject = GameObject.Instantiate(PrefabFactory.Create(EntityPrefab.MessageBoxText));
			IUIText uiText = RetrieveUITextComponent(textObject); 
			uiText.Text = text;

			//Parent to the message box.
			ChatWindow.Parent(textObject);
		}

		private static IUIText RetrieveUITextComponent([NotNull] GameObject textObject)
		{
			if (textObject == null) throw new ArgumentNullException(nameof(textObject));

			//BIG problems if this doesn't exist.
			IUIText text = textObject.GetComponent<IUIText>();

			if (text == null)
				throw new InvalidOperationException($"Faile to load {nameof(IUIText)} from gameobject {textObject.name}");

			return text;
		}

		public async Task OnGameInitialized()
		{
			//Hack to get into scene
		}
	}
}
