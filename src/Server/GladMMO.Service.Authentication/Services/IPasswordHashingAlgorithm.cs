using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that implement password hashing algorithms.
	/// </summary>
	public interface IPasswordHashingAlgorithm
	{
		/// <summary>
		/// Creates a hash based on the inputs.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		string CreateHash(string username, string password);
	}
}
