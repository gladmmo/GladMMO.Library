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
	[AdditionalRegisterationAs(typeof(ICharacterCreationButtonClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	public sealed class CharacterCreationButtonClickedGlueStartable : BaseButtonClickGlueStartable, ICharacterCreationButtonClickedEventSubscribable
	{
		public const UnityUIRegisterationKey ButtonKey = UnityUIRegisterationKey.BackButton;

		public CharacterCreationButtonClickedGlueStartable([KeyFilter(ButtonKey)] [NotNull] IUIButton createButton) 
			: base(createButton)
		{

		}
	}
}
