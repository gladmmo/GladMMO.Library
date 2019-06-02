namespace GladMMO
{
	/// <summary>
	/// Enumeration of all character session enter/creat responses.
	/// </summary>
	public enum CharacterSessionEnterResponseCode
	{
		/// <summary>
		/// Indicates that a session enter request was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that an invalid character id was provided
		/// as a reques to to create a session.
		/// </summary>
		InvalidCharacterIdError = 3,

		/// <summary>
		/// While this seems unlike/impossible it's possible that the account tied to a characterid
		/// may not have a session on the character but
		/// may be on another character.
		/// </summary>
		AccountAlreadyHasCharacterSession = 4,

		/// <summary>
		/// Result of an unknown server error.
		/// </summary>
		GeneralServerError = 5
	}
}