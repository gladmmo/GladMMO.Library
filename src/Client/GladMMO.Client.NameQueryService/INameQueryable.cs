using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityNameQueryable : INameQueryService
	{
		/// <summary>
		/// Ensures a name is known/exists with the <see cref="NetworkEntityGuid"/> 
		/// </summary>
		/// <param name="entity">The entity guid</param>
		/// <exception cref="KeyNotFoundException">Thrown if the <see cref="entity"/> is not known.</exception>
		void EnsureExists(NetworkEntityGuid entity);

		/// <summary>
		/// Retrieves the name of the entity
		/// from the provided <see cref="entity"/>.
		/// </summary>
		/// <param name="entity">The id of the entity.</param>
		/// <exception cref="KeyNotFoundException">Throws if the key is not found.</exception>
		/// <returns>The name.</returns>
		string Retrieve(NetworkEntityGuid entity);
	}
}
