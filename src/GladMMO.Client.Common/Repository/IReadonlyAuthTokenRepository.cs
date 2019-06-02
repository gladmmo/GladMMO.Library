using System;

namespace GladMMO
{
	public interface IReadonlyAuthTokenRepository
	{
		/// <summary>
		/// Retrieves the auth token.
		/// </summary>
		/// <returns>The authentication token.</returns>
		/// <exception cref="InvalidOperationException">Will throw if no auth token is registered.</exception>
		string Retrieve();

		/// <summary>
		/// Similar to <see cref="Retrieve"/> but includes the token type.
		/// Such as Bearer.
		/// </summary>
		/// <returns></returns>
		string RetrieveWithType();

		/// <summary>
		/// Gets the scheme/type of authentication for the token
		/// and only the scheme/type.
		/// </summary>
		/// <returns></returns>
		string RetrieveType();
	}
}