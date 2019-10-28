using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ISceneBackButtonClickedSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SceneBackButtonClickedGlueStartable : BaseButtonClickGlueStartable, ISceneBackButtonClickedSubscribable
	{
		public const UnityUIRegisterationKey ButtonKey = UnityUIRegisterationKey.BackButton;

		public SceneBackButtonClickedGlueStartable([KeyFilter(ButtonKey)] [NotNull] IUIButton createButton)
			: base(createButton)
		{

		}
	}
}