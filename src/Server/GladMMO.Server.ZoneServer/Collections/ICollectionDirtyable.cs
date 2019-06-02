using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICollectionMapDirtyable<in TKey>
	{
		/// <summary>
		/// Indicates if the entry with the provided <see cref="key"/> is dirty.
		/// Should return false if it the collection does not contain the key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool isEntryDirty(TKey key);

		/// <summary>
		/// Sets the provided <see cref="key"/>'s entry as
		/// <see cref="isDirty"/>.
		/// </summary>
		/// <param name="key">The key for the entry to change the dirty state for.</param>
		/// <param name="isDirty">Whether to set it dirty or not.</param>
		void SetDirtyState(TKey key, bool isDirty);

		/// <summary>
		/// Clears the dirty state of all entries.
		/// </summary>
		void ClearDirty();
	}
}
