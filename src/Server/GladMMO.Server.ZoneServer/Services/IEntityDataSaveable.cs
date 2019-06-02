using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for a service that can save entity data.
	/// </summary>
	public interface IEntityDataSaveable
	{
		//TODO: Maybe create a result, so failed saves can be handled
		/// <summary>
		/// Save the data.
		/// </summary>
		void Save(NetworkEntityGuid guid);

		/// <summary>
		/// Async save the entity data.
		/// </summary>
		/// <returns></returns>
		Task SaveAsync(NetworkEntityGuid guid);
	}
}
