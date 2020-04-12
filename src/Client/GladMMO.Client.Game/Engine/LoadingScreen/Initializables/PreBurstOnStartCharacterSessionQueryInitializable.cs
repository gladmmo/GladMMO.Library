using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	/// <summary>
	/// Basically, all this does is query for the character session data and broadcast a change event related to it.
	/// </summary>
	[AdditionalRegisterationAs(typeof(ICharacterSessionDataChangedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class PreBurstOnStartCharacterSessionQueryStartable : IGameStartable, ICharacterSessionDataChangedEventSubscribable
	{
		//TODO: Refactor this behind its own object to provide download URL for character.
		private ICharacterService CharacterService { get; }

		private ILocalCharacterDataRepository LocalCharacterData { get; }

		private ILog Logger { get; }

		public event EventHandler<CharacterSessionDataChangedEventArgs> OnCharacterSessionDataChanged;

		public PreBurstOnStartCharacterSessionQueryStartable(
			[NotNull] ICharacterService characterService,
			[NotNull] ILocalCharacterDataRepository localCharacterData,
			[NotNull] ILog logger)
		{
			CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
			LocalCharacterData = localCharacterData ?? throw new ArgumentNullException(nameof(localCharacterData));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task OnGameStart()
		{
			//When we start the loading screen for the game
			//To know what world we should load we should
			//To know that we need information about the character session.
			CharacterSessionEnterResponse characterSessionData = await CharacterService.TryEnterSession(LocalCharacterData.LocalCharacterGuid.CurrentObjectGuid)
				.ConfigureAwaitFalse();

			if(!characterSessionData.isSuccessful)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to query Character Session Data: {characterSessionData.ResultCode}:{(int)characterSessionData.ResultCode}");

				//If we already have a claim we should repeat.
				if (characterSessionData.ResultCode == CharacterSessionEnterResponseCode.AccountAlreadyHasCharacterSession)
				{
					//Retry 5 times while not successful.
					for (int i = 0; i < 5 && !characterSessionData.isSuccessful; i++)
					{
						characterSessionData = await CharacterService.TryEnterSession(LocalCharacterData.LocalCharacterGuid.CurrentObjectGuid)
							.ConfigureAwaitFalse();

						await Task.Delay(1500)
							.ConfigureAwaitFalseVoid();
					}

					//If not succesful after the retry.
					if (!characterSessionData.isSuccessful)
					{
						await LoadCharacterSelection();
						return;
					}
				}
				else
				{
					await LoadCharacterSelection();
				}
					
			}

			if(Logger.IsInfoEnabled)
				Logger.Info($"About to broadcasting {nameof(OnCharacterSessionDataChanged)} with Zone: {characterSessionData.ZoneId}");

			OnCharacterSessionDataChanged?.Invoke(this, new CharacterSessionDataChangedEventArgs(characterSessionData.ZoneId));
		}

		private static async Task LoadCharacterSelection()
		{
			await new UnityYieldAwaitable();

			//TODO: Broadcast failure, don't hackily just load scene again.
			//TODO: Don't hackily hardcode the scene number.
			SceneManager.LoadSceneAsync(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME).allowSceneActivation = true;
		}
	}
}
