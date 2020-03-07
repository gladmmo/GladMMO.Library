using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IRegisterAccountButtonClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	public sealed class RegisterAccountButtonClickedGlueStartable : BaseButtonClickGlueStartable, IRegisterAccountButtonClickedEventSubscribable
	{
		public const UnityUIRegisterationKey ButtonKey = UnityUIRegisterationKey.Registeration;

		public RegisterAccountButtonClickedGlueStartable([KeyFilter(ButtonKey)] [NotNull] IUIButton createButton) 
			: base(createButton)
		{

		}
	}
}
