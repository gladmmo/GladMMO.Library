namespace GladMMO
{
	/// <summary>
	/// Enumeration of all possible response codes
	/// for <see cref="CharacterNameValidationResponse"/>
	/// </summary>
	public enum CharacterNameValidationResponseCode
	{
		/// <summary>
		/// Indicates that the name is valid.
		/// </summary>
		Success = 1,
		
		//TODO: Doc
		NameIsUnavailable = 2,

		NameContainsInvalidCharacters = 3,

		NameLengthIsInvalid = 4,
	}
}