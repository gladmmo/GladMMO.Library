using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IEntityNameQueryable
	{
		/// <summary>
		/// Ensures a name is known/exists with the <see cref="ObjectGuid"/> 
		/// </summary>
		/// <param name="entity">The entity guid</param>
		/// <exception cref="KeyNotFoundException">Thrown if the <see cref="entity"/> is not known.</exception>
		void EnsureExists(ObjectGuid entity);

		/// <summary>
		/// Retrieves the name of the entity
		/// from the provided <see cref="entity"/>.
		/// </summary>
		/// <param name="entity">The id of the entity.</param>
		/// <exception cref="KeyNotFoundException">Throws if the key is not found.</exception>
		/// <returns>The name.</returns>
		string Retrieve(ObjectGuid entity);

		/// <summary>
		/// Retrieves the name of the entity
		/// from the provided <see cref="entity"/>.
		/// </summary>
		/// <param name="entity">The id of the entity.</param>
		/// <exception cref="KeyNotFoundException">Throws if the key is not found.</exception>
		/// <returns>The name.</returns>
		Task<string> RetrieveAsync(ObjectGuid entity);

		/// <summary>
		/// Indicates a name is known/exists with the <see cref="ObjectGuid"/>.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		bool Exists(ObjectGuid entity);
	}
}
