using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	public static class InterestCollectionExtensions
	{
		/// <summary>
		/// Determines if the interest collection has pending interest changes.
		/// (Joining or leaving the collection via queues).
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static bool HasPendingChanges([NotNull] this InterestCollection collection)
		{
			if(collection == null) throw new ArgumentNullException(nameof(collection));

			return !collection.EnteringDequeueable.isEmpty || !collection.LeavingDequeueable.isEmpty;
		}
	}
}
