namespace GladMMO
{
	/// <summary>
	/// Enumeration of all response codes for a <see cref="ClientSessionClaimResponsePayload"/>
	/// </summary>
	public enum ClientSessionClaimResponseCode
	{
		//Don't use 0 as success
		Success = 1,

		SessionAlreadyClaimed = 2,

		SessionUnavailable = 3,

		SessionExpired = 4
	}
}