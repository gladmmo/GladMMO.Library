using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of keys for Unity UI
	/// IoC container registeration.
	/// </summary>
	public enum UnityUIRegisterationKey
	{
		Unknown = 0,

		PlayerUnitFrame = 1,

		UsernameTextBox = 2,

		PasswordTextBox = 3,

		Login = 4,

		PlayerHealthBar = 6,

		Registeration = 7,

		ChatBox = 8,

		CharacterSelection = 9,

		ChatInput = 10,

		GroupUnitFrames = 11,

		EnterWorld = 12,

		/// <summary>
		/// The parent of all text objects for text chat.
		/// </summary>
		TextChatParentWindow = 13,

		LoginTextBox = 14,

		WorldDownloadProgress = 15,

		CharacterCreateButton = 16,

		BackButton = 17,

		CharacterNameInput = 18,

		ErrorTitle = 19,

		ErrorMessage = 20,

		ErrorOkButton = 21,

		ErrorBox = 22,

		TargetUnitFrame = 23,

		ExperienceBar = 24,

		FriendsWindow = 25,

		SocialWindowAddFriendButton = 26,

		AddFriendModalWindow = 27,

		GuildList = 28,

		SocialWindowAddGuildMember = 29,

		AddGuildMemberModalWindow = 30,
	}
}
