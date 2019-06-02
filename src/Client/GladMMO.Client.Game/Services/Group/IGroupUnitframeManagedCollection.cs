using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IGroupUnitframeManagedCollection
	{
		/// <summary>
		/// Indicates a provided guid <see cref="entity"/>
		/// has a unitframe associated with it.
		/// </summary>
		/// <param name="entity">The entity guid.</param>
		/// <returns>True if the entity guid has a unitframe associated with it.</returns>
		bool Contains(NetworkEntityGuid entity);

		/// <summary>
		/// Gets a <see cref="IUIUnitFrame"/>
		/// from the collection.
		/// </summary>
		/// <param name="entity">The entity guid.</param>
		/// <returns></returns>
		IUIUnitFrame this[NetworkEntityGuid entity] { get; }
	}
}
