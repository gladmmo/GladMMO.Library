namespace GladMMO
{
	public enum CharacterSessionDataResponseCode
	{
		Success = 1,

		NoSessionAvailable = 2,

		Unauthorized = 3,

		/// <summary>
		/// Potentially represents an error on the gameserver.
		/// </summary>
		GeneralServerError = 4,
	}
}