using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GladMMO
{
	/// <summary>
	/// More <see cref="System.Linq"/> extensions for the Guardians project.
	/// </summary>
	public static class GuardiansExtendedLINQ
	{
		/// <summary>
		/// Extension that attempts to downcast the provided <see cref="collection"/>
		/// to an array if it is an array. Otherwise it will call <see cref="Enumerable"/>.ToArray().
		/// </summary>
		/// <typeparam name="T">The element type of the collection.</typeparam>
		/// <param name="collection">The collection to cast.</param>
		/// <returns></returns>
		public static T[] ToArrayTryAvoidCopy<T>(this IEnumerable<T> collection)
		{
			if(collection == null) throw new ArgumentNullException(nameof(collection));

			if(collection is T[] downcastedArray)
				return downcastedArray;

			return collection.ToArray();
		}
	}
}
