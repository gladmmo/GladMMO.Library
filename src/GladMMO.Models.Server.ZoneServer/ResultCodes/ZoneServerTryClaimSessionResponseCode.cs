namespace GladMMO
{
	public enum ZoneServerTryClaimSessionResponseCode
	{
		//TODO: When we enable SQL returns for result we will add the other codes
		Success = 1,

		ZoneRegisterationDoesNotExistError = 2,

		SessionInvalidError = 3,

		GeneralServerError = 4,
	}
}