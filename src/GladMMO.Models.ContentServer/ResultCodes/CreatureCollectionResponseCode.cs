namespace GladMMO
{
	/// <summary>
	/// Enumeration of all response codes associated with looking up creature data
	/// in collection form.
	/// </summary>
	public enum CreatureCollectionResponseCode
	{
		Success = 1,

		/// <summary>
		/// Indicates that no entries were found.
		/// </summary>
		NoneFound = 2,

		//TODO: Implement potential error codes
	}
}