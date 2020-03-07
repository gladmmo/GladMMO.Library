using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IManualInviteGuildMemberClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ManualInviteGuildMemberButtonClickedGlueStartable : BaseButtonClickGlueStartable, IManualInviteGuildMemberClickedEventSubscribable
	{
		public const UnityUIRegisterationKey ButtonKey = UnityUIRegisterationKey.SocialWindowAddGuildMember;

		public ManualInviteGuildMemberButtonClickedGlueStartable([KeyFilter(ButtonKey)] [NotNull] IUIButton createButton)
			: base(createButton)
		{

		}
	}
}
