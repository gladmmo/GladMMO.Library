namespace GladMMO
{
	/// <summary>
	/// Enumeration of potential results of a name query.
	/// </summary>
	public enum NameQueryResponseCode
	{
		/// <summary>
		/// Indicates success.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that the id requested as unknown.
		/// </summary>
		UnknownIdError = 2,

		GeneralServerError = 3
	}
}