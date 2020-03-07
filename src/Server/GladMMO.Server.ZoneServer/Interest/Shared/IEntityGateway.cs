using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a gateway entites can enter/exit.
	/// </summary>
	/// <typeparam name="TEntryContext"></typeparam>
	public interface IEntityGateway<in TEntryContext>
	{
		/// <summary>
		/// Attempts to enter the entity through the gateway with the
		/// provided <see cref="entryContext"/> information.
		/// The entity is represented by its <see cref="entityGuid"/>.
		/// </summary>
		/// <param name="entryContext">The entry context.</param>
		/// <param name="entityGuid">The entity guid.</param>
		/// <returns>True if the entity enters successfully.</returns>
		bool TryEntityEnter(TEntryContext entryContext, ObjectGuid entityGuid);

		/// <summary>
		/// Attempts to exit the entity through the gateway with the
		/// provided <see cref="entryContext"/> information.
		/// The entity is represented by its <see cref="entityGuid"/>.
		/// </summary>
		/// <param name="entryContext">The entry context.</param>
		/// <param name="entityGuid">The entity guid.</param>
		/// <returns>True if the entity exits successfully.</returns>
		bool TryEntityLeave(TEntryContext entryContext, ObjectGuid entityGuid);
	}
}
