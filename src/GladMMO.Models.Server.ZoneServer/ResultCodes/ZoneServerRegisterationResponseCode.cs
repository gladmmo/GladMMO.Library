namespace GladMMO
{
	public enum ZoneServerRegisterationResponseCode
	{
		Success = 1,

		/// <summary>
		/// Indicates that there is no job/work for the instance server to do.
		/// </summary>
		NoWorkTodo = 2,

		/// <summary>
		/// Don't know what is wrong here, but registeration failed.
		/// That's all that is known.
		/// </summary>
		FailedToRegister = 3,
	}
}