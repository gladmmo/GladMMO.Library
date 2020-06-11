using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac.Features.AttributeFilters;
using FreecraftCore;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LoadingScreenDisplayEventListener : BaseSingleEventListenerInitializable<IRequestedSceneChangeEventSubscribable, RequestedSceneChangeEventArgs>
	{
		private ILoadingScreenManagementService LoadingScreenService { get; }

		public LoadingScreenDisplayEventListener([NotNull] IRequestedSceneChangeEventSubscribable subscriptionService,
			[NotNull] ILoadingScreenManagementService loadingScreenService) 
			: base(subscriptionService)
		{
			LoadingScreenService = loadingScreenService ?? throw new ArgumentNullException(nameof(loadingScreenService));
		}

		protected override void OnEventFired(object source, RequestedSceneChangeEventArgs args)
		{
			if (args.isLoadingSpecificMap)
			{
				LoadingScreenService.EnableLoadingScreenForMap(args.MapId);
			}
		}
	}
}
