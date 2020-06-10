using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
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
	public sealed class GeneralErrorUIDispatchEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<IGeneralErrorEncounteredEventSubscribable, GeneralErrorEncounteredEventArgs>, IDisposable
	{
		public IUIText ErrorTitle { get; }

		public IUIText ErrorMessage { get; }

		public IUIButton ErrorButton { get; }

		public IUIElement ErrorDialogBox { get; }

		private ILog Logger { get; }

		//We don't use a stack because we want to handle errors
		//first in first out. This means we could have SERVE errors in front of minor errors so we
		//want to handle those first.
		//Eventually we should do a priority queue.
		private Queue<GeneralErrorEncounteredEventArgs> ErrorArgsQueue { get; }

		//TODO: Find a better way to unregister.
		private Action OnErrorButtonPressedOkPersistedAction { get; }

		public GeneralErrorUIDispatchEventListener([NotNull] IGeneralErrorEncounteredEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.ErrorTitle)] [NotNull] IUIText errorTitle,
			[KeyFilter(UnityUIRegisterationKey.ErrorMessage)] [NotNull] IUIText errorMessage,
			[KeyFilter(UnityUIRegisterationKey.ErrorOkButton)] [NotNull] IUIButton errorButton,
			[KeyFilter(UnityUIRegisterationKey.ErrorBox)] [NotNull] IUIElement errorDialogBox,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			if (subscriptionService == null) throw new ArgumentNullException(nameof(subscriptionService));
			ErrorTitle = errorTitle ?? throw new ArgumentNullException(nameof(errorTitle));
			ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
			ErrorButton = errorButton ?? throw new ArgumentNullException(nameof(errorButton));
			ErrorDialogBox = errorDialogBox ?? throw new ArgumentNullException(nameof(errorDialogBox));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			ErrorArgsQueue = new Queue<GeneralErrorEncounteredEventArgs>(2);

			//TODO: Find a better way to unregister.
			OnErrorButtonPressedOkPersistedAction = OnErrorOkButtonClicked;

			//Register callback into this dispatcher.
			ErrorButton.AddOnClickListener(OnErrorButtonPressedOkPersistedAction);

			if(logger.IsDebugEnabled)
				Logger.Debug($"Creating Error Handler for Scene: {SceneManager.GetActiveScene().name}");
		}

		private void OnErrorOkButtonClicked()
		{
			try
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Error Frame OK. Error Args Count: {ErrorArgsQueue.Count}.");

				//They clicked ok.
				//Dispatch the action if any in the current top of the queue
				//and we need to handle the next error too
				ErrorArgsQueue.Dequeue().OnButtonClicked?.Invoke();

				if(ErrorArgsQueue.Any())
					InitializeErrorMenu(ErrorArgsQueue.Peek());
				else
					ErrorDialogBox.SetElementActive(false);
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Encountered Error in Error Handler. Error: {e.Message}");

				throw;
			}
		}

		protected override void OnThreadUnSafeEventFired(object source, GeneralErrorEncounteredEventArgs args)
		{
			if(Logger.IsWarnEnabled)
				Logger.Warn($"General Error Encountered. Processing.");

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

		public void Dispose()
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Unregistered General Error hook.");

			ErrorButton.RemoveOnClickListener(OnErrorButtonPressedOkPersistedAction);
		}
	}
}
