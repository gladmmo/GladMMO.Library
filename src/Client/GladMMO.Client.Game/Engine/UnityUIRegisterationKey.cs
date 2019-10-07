﻿using System;
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
	}
}
