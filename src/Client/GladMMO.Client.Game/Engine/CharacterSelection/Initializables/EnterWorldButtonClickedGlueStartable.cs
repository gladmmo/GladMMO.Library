using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEnterWorldButtonClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class EnterWorldButtonClickedGlueStartable : BaseButtonClickGlueStartable, IEnterWorldButtonClickedEventSubscribable
	{
		public const UnityUIRegisterationKey ButtonKey = UnityUIRegisterationKey.EnterWorld;

		public EnterWorldButtonClickedGlueStartable([KeyFilter(ButtonKey)] [NotNull] IUIButton createButton)
			: base(createButton)
		{

		}
	}
}
