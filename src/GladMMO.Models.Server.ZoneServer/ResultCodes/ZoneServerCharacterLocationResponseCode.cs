namespace GladMMO
{
	/// <summary>
	/// Enumeration of response codes for <see cref="ZoneServerCharacterLocationResponse"/>.
	/// </summary>
	public enum ZoneServerCharacterLocationResponseCode
	{
		Success = 1,
		CharacterDoesntExist = 2,
		GeneralError = 3,
		NoLocationDefined = 4
	}
}