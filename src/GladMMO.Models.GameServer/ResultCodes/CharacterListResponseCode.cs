namespace GladMMO
{
	/// <summary>
	/// Enumeration of all potential character list response results.
	/// </summary>
	public enum CharacterListResponseCode
	{
		/// <summary>
		/// Indicates that the character list response
		/// was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that no characters were found.
		/// </summary>
		NoCharactersFoundError = 2,

		/// <summary>
		/// Indicates an unknown/general error occurred.
		/// </summary>
		GeneralServerError = 3
	}
}