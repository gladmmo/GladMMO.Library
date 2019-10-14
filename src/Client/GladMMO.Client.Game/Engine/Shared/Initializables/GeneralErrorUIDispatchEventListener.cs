using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class GeneralErrorUIDispatchEventListener : BaseSingleEventListenerInitializable<IGeneralErrorEncounteredEventSubscribable, GeneralErrorEncounteredEventArgs>
	{
		public IUIText ErrorTitle { get; }

		public IUIText ErrorMessage { get; }

		public IUIButton ErrorButton { get; }

		public IUIElement ErrorDialogBox { get; }

		public GeneralErrorUIDispatchEventListener([NotNull] IGeneralErrorEncounteredEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.ErrorTitle)] [NotNull] IUIText errorTitle,
			[KeyFilter(UnityUIRegisterationKey.ErrorMessage)] [NotNull] IUIText errorMessage,
			[KeyFilter(UnityUIRegisterationKey.ErrorOkButton)] [NotNull] IUIButton errorButton,
			[KeyFilter(UnityUIRegisterationKey.ErrorBox)] [NotNull] IUIElement errorDialogBox)
			: base(subscriptionService)
		{
			if (subscriptionService == null) throw new ArgumentNullException(nameof(subscriptionService));
			ErrorTitle = errorTitle ?? throw new ArgumentNullException(nameof(errorTitle));
			ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
			ErrorButton = errorButton ?? throw new ArgumentNullException(nameof(errorButton));
			ErrorDialogBox = errorDialogBox ?? throw new ArgumentNullException(nameof(errorDialogBox));
		}

		protected override void OnEventFired(object source, GeneralErrorEncounteredEventArgs args)
		{
			//Error is recieved, we don't know what thread this is on but maybe it's threadsafe
			//to set UI text.
			ErrorTitle.Text = args.ErrorTitle;
			ErrorMessage.Text = args.ErrorMessage;
			ErrorDialogBox.SetElementActive(true);

			//TODO: Support customizable actions.
			ErrorButton.AddOnClickListener(() => SceneManager.LoadScene(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME));
		}
	}
}
