namespace GladMMO
{
	/// <summary>
	/// Enumeration of result codes
	/// for a Waypoint query response.
	/// </summary>
	public enum WaypointQueryResponseCode
	{
		/// <summary>
		/// Indicates success.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that the entry was not found.
		/// </summary>
		EntryNotFound = 2,

		/// <summary>
		/// Indicates that a general server error occured.
		/// </summary>
		GeneralServerError = 3
	}
}