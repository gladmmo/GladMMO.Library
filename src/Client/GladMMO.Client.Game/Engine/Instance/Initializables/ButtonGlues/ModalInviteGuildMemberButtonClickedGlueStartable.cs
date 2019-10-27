using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	//This is the button event for when they hit the "Add" button on the Invite GuildMember popup modal.
	[AdditionalRegisterationAs(typeof(IInviteGuildMemberModalClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ModalInviteGuildMemberButtonClickedGlueStartable : BaseButtonClickGlueStartable, IInviteGuildMemberModalClickedEventSubscribable
	{
		public const UnityUIRegisterationKey ButtonKey = UnityUIRegisterationKey.AddGuildMemberModalWindow;

		public ModalInviteGuildMemberButtonClickedGlueStartable([KeyFilter(ButtonKey)] [NotNull] IUIButton createButton)
			: base(createButton)
		{

		}
	}
}
