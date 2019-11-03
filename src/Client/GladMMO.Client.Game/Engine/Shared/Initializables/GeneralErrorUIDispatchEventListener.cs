using System;
using System.Collections.Generic;
using System.Linq;
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
	public sealed class GeneralErrorUIDispatchEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<IGeneralErrorEncounteredEventSubscribable, GeneralErrorEncounteredEventArgs>
	{
		public IUIText ErrorTitle { get; }

		public IUIText ErrorMessage { get; }

		public IUIButton ErrorButton { get; }

		public IUIElement ErrorDialogBox { get; }

		//We don't use a stack because we want to handle errors
		//first in first out. This means we could have SERVE errors in front of minor errors so we
		//want to handle those first.
		//Eventually we should do a priority queue.
		private Queue<GeneralErrorEncounteredEventArgs> ErrorArgsQueue { get; }

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

			ErrorArgsQueue = new Queue<GeneralErrorEncounteredEventArgs>(2);
			//Register callback into this dispatcher.
			ErrorButton.AddOnClickListener(OnErrorOkButtonClicked);
		}

		private void OnErrorOkButtonClicked()
		{
			//They clicked ok.
			//Dispatch the action if any in the current top of the queue
			//and we need to handle the next error too
			ErrorArgsQueue.Dequeue().OnButtonClicked?.Invoke();

			if (ErrorArgsQueue.Any())
				InitializeErrorMenu(ErrorArgsQueue.Peek());
			else
				ErrorDialogBox.SetElementActive(false);
		}

		protected override void OnThreadUnSafeEventFired(object source, GeneralErrorEncounteredEventArgs args)
		{
			ErrorArgsQueue.Enqueue(args);

			//if the one we added is the only one we should set the values.
			if(ErrorArgsQueue.Count == 1)
				InitializeErrorMenu(args);
		}

		private void InitializeErrorMenu(GeneralErrorEncounteredEventArgs args)
		{
			//Error is recieved, we don't know what thread this is on but maybe it's threadsafe
			//to set UI text.
			ErrorTitle.Text = args.ErrorTitle;
			ErrorMessage.Text = args.ErrorMessage;
			ErrorDialogBox.SetElementActive(true);
		}
	}
}
