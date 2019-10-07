using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	public sealed class CharacterCreationBackButtonStartable : IGameStartable
	{
		private IUIButton BackButton { get; }

		public CharacterCreationBackButtonStartable(
			[KeyFilter(UnityUIRegisterationKey.BackButton)] [NotNull] IUIButton backButton)
		{
			BackButton = backButton ?? throw new ArgumentNullException(nameof(backButton));
		}

		public async Task OnGameStart()
		{
			await new UnityYieldAwaitable();
			BackButton.AddOnClickListener(() => { SceneManager.LoadSceneAsync(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME).allowSceneActivation = true; });
		}
	}
}
