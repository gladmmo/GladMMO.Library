using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class SetDownloadProgressEventListener : BaseSingleEventListenerInitializable<IWorldDownloadBeginEventSubscribable, WorldDownloadBeginEventArgs>, IGameTickable
	{
		public IUIFillableImage DownloadFillImage { get; }

		public IUIText DownloadText { get; }

		private AsyncOperation CurrentDownloadOperation { get; set; } = null;

		public SetDownloadProgressEventListener(IWorldDownloadBeginEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.WorldDownloadProgress)] [NotNull] IUIFillableImage downloadFillImage,
			[KeyFilter(UnityUIRegisterationKey.WorldDownloadProgress)] [NotNull] IUIText downloadText) 
			: base(subscriptionService)
		{
			DownloadFillImage = downloadFillImage ?? throw new ArgumentNullException(nameof(downloadFillImage));
			DownloadText = downloadText ?? throw new ArgumentNullException(nameof(downloadText));
		}

		protected override void OnEventFired(object source, WorldDownloadBeginEventArgs args)
		{
			CurrentDownloadOperation = args.DownloadOperation;
		}

		public void Tick()
		{
			//Can't do anything without world downloading operation.
			if (CurrentDownloadOperation == null)
				return;

			DownloadText.Text = $"{CurrentDownloadOperation.progress * 100}%";
			DownloadFillImage.FillAmount = CurrentDownloadOperation.progress;
		}
	}
}
