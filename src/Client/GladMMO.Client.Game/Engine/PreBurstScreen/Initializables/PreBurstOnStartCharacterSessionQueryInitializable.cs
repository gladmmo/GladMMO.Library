using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Basically, all this does is query for the character session data and broadcast a change event related to it.
	/// </summary>
	[AdditionalRegisterationAs(typeof(ICharacterSessionDataChangedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class PreBurstOnStartCharacterSessionQueryInitializable : IGameInitializable, ICharacterSessionDataChangedEventSubscribable
	{
		//TODO: Refactor this behind its own object to provide download URL for character.
		private ICharacterService CharacterService { get; }

		private ICharacterDataRepository LocalCharacterData { get; }

		private ILog Logger { get; }

		public event EventHandler<CharacterSessionDataChangedEventArgs> OnCharacterSessionDataChanged;

		public PreBurstOnStartCharacterSessionQueryInitializable(
			[NotNull] ICharacterService characterService,
			[NotNull] ICharacterDataRepository localCharacterData,
			[NotNull] ILog logger)
		{
			CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
			LocalCharacterData = localCharacterData ?? throw new ArgumentNullException(nameof(localCharacterData));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task OnGameInitialized()
		{
			//When we start the loading screen for the game
			//To know what world we should load we should
			//To know that we need information about the character session.
			CharacterSessionDataResponse characterSessionData = await CharacterService.GetCharacterSessionData(LocalCharacterData.CharacterId)
				.ConfigureAwait(false);

			if(!characterSessionData.isSuccessful)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to query Character Session Data: {characterSessionData.ResultCode}:{(int)characterSessionData.ResultCode}");
				return;
			}

			if(Logger.IsInfoEnabled)
				Logger.Info($"About to broadcasting {nameof(OnCharacterSessionDataChanged)} with Zone: {characterSessionData.ZoneId}");

			OnCharacterSessionDataChanged?.Invoke(this, new CharacterSessionDataChangedEventArgs(characterSessionData.ZoneId));
		}
	}
}
