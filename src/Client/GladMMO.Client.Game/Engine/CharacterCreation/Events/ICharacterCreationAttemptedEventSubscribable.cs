using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICharacterCreationAttemptedEventSubscribable
	{
		event EventHandler<CharacterCreationAttemptedEventArgs> OnCharacterCreationAttempted;
	}

	//TODO: Come up with more uniformed design of failable event notifications.
	public sealed class CharacterCreationAttemptedEventArgs : EventArgs
	{
		/// <summary>
		/// Indicates if the character creation attempt was successful.
		/// </summary>
		public bool isSuccessful => !OptionalErrorCode.HasValue;

		/// <summary>
		/// The name of the attempted character creation.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Indicates the optional error code for failed attempts.
		/// Set only when <see cref="isSuccessful"/> is false.
		/// </summary>
		public CharacterCreationResponseCode? OptionalErrorCode { get; }

		public CharacterCreationAttemptedEventArgs(string name, CharacterCreationResponseCode? optionalErrorCode = CharacterCreationResponseCode.Success)
		{
			Name = name;
			OptionalErrorCode = optionalErrorCode == CharacterCreationResponseCode.Success ? null : optionalErrorCode;
		}
	}
}
