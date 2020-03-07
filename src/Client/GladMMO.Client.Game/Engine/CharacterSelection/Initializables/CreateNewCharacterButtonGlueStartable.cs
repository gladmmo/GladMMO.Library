using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ICreateNewCharacterButtonClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class CreateNewCharacterButtonGlueStartable : BaseButtonClickGlueStartable, ICreateNewCharacterButtonClickedEventSubscribable
	{
		public CreateNewCharacterButtonGlueStartable([KeyFilter(UnityUIRegisterationKey.CharacterCreateButton)] IUIButton referenceButton) 
			: base(referenceButton)
		{
		}
	}
}
