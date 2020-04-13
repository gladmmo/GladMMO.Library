using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class LoadingScreenDisplayEventListener : BaseSingleEventListenerInitializable<IRequestedSceneChangeEventSubscribable, RequestedSceneChangeEventArgs>
	{
		public IUIElement LoadingScreenRoot { get; }

		public IUIImage LoadingScreenBackgroundImage { get; }

		public IClientDataCollectionContainer ClientData { get; }

		public LoadingScreenDisplayEventListener([NotNull] IRequestedSceneChangeEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIElement loadingScreenRoot,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIImage loadingScreenBackgroundImage,
			[NotNull] IClientDataCollectionContainer clientData) 
			: base(subscriptionService)
		{
			LoadingScreenRoot = loadingScreenRoot ?? throw new ArgumentNullException(nameof(loadingScreenRoot));
			LoadingScreenBackgroundImage = loadingScreenBackgroundImage ?? throw new ArgumentNullException(nameof(loadingScreenBackgroundImage));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
		}

		protected override void OnEventFired(object source, RequestedSceneChangeEventArgs args)
		{
			if (args.isLoadingSpecificMap)
			{
				var mapEntry = ClientData.MapEntry[args.MapId];
				if (ClientData.LoadingScreens.Contains(mapEntry.LoadingScreenId))
				{
					//TODO: Handle widescreen
					var loadingScreen = ClientData.LoadingScreens[mapEntry.LoadingScreenId];

					//Note: Extensions must be omitted. https://docs.unity3d.com/ScriptReference/Resources.Load.html
					string imagePath = Path.ChangeExtension(loadingScreen.FilePath.GetString(), null);

					LoadingScreenBackgroundImage.SetSpriteTexture(Resources.Load<Texture2D>(imagePath));
				}
			}

			LoadingScreenRoot.SetElementActive(true);
		}
	}
}
