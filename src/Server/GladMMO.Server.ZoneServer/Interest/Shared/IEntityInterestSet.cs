namespace GladMMO
{
	/// <summary>
	/// Add/Remove interface for an interest set.
	/// (We don't use ICollection because of all the ridiculous methods like CopyTo and others)
	/// </summary>
	public interface IEntityInterestSet
	{
		/// <summary>
		/// Adds an entity to the interest set.
		/// </summary>
		/// <param name="guid">The entity to add.</param>
		/// <returns>True if the entity was added. False if it is already contained.</returns>
		bool Add(NetworkEntityGuid guid);

		/// <summary>
		/// Tries to remove an entity in the interest set.
		/// If it is in the interest set, and successfully removed, it will return true.
		/// If it not contained it will return false.
		/// </summary>
		/// <param name="guid">The entity to add.</param>
		/// <returns>True if the entity was found and removed.</returns>
		bool Remove(NetworkEntityGuid guid);
	}
}