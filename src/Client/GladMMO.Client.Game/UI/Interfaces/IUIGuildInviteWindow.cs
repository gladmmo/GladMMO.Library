using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIGuildInviteWindow : IUIElement //it's attached to the window.
	{
		IUIButton DeclineInviteButton { get; }

		IUIButton AcceptInviteButton { get; }

		IUIText InvitationText { get; }

		IUIText GuildNameText { get; }
	}
}
