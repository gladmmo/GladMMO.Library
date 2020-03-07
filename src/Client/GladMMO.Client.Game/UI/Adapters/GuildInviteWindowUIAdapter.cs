using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.UI;

namespace GladMMO
{
#pragma warning disable 649
	public sealed class GuildInviteWindowUIAdapter : BaseUnityUI<IUIGuildInviteWindow>, IUIGuildInviteWindow
	{
		[SerializeField]
		private Button _declineInviteButton;

		[SerializeField]
		private Button _acceptInviteButton;

		[SerializeField]
		private Text _invitationText;

		[SerializeField]
		private Text _guildNameText;

		private Lazy<IUIButton> _DeclineInviteButton { get; }
			    
		private Lazy<IUIButton> _AcceptInviteButton { get; }
			    
		private Lazy<IUIText> _InvitationText { get; }
			    
		private Lazy<IUIText> _GuildNameText { get; }

		public IUIButton DeclineInviteButton => _DeclineInviteButton.Value;

		public IUIButton AcceptInviteButton => _AcceptInviteButton.Value;

		public IUIText InvitationText => _InvitationText.Value;

		public IUIText GuildNameText => _GuildNameText.Value;

		public GuildInviteWindowUIAdapter()
		{
			_DeclineInviteButton = new Lazy<IUIButton>(() => new UnityButtonUIButtonAdapterImplementation(_declineInviteButton));
			_AcceptInviteButton = new Lazy<IUIButton>(() => new UnityButtonUIButtonAdapterImplementation(_acceptInviteButton));

			_InvitationText = new Lazy<IUIText>(() => new UnityTextUITextAdapterImplementation(_invitationText));
			_GuildNameText = new Lazy<IUIText>(() => new UnityTextUITextAdapterImplementation(_guildNameText));
		}

		public void SetElementActive(bool state)
		{
			gameObject.SetActive(state);
		}

		public bool isActive => gameObject.activeSelf;
	}
#pragma warning restore 649
}
