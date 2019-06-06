using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GladMMO
{
	public static class EntityMappableExtensions
	{
		/// <summary>
		/// Adds the entity object with the provided key <see cref="guid"/>
		/// </summary>
		/// <typeparam name="TReturnType"></typeparam>
		/// <param name="collection"></param>
		/// <param name="guid"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddObject<TReturnType>([NotNull] this IDictionary<NetworkEntityGuid, TReturnType> collection, [NotNull] NetworkEntityGuid guid, TReturnType obj)
			where TReturnType : class
		{
			//No null checking because we hope to inline this.
			try
			{
				collection[guid] = obj; //replaces existing value if there is any.
			}
			catch(Exception e)
			{
				CreateEntityNotFoundException<TReturnType>(guid, e);
			}
		}

		private static Exception CreateEntityNotFoundException<TReturnType>(NetworkEntityGuid guid, Exception e)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid), $"Found that provided entity guid in {nameof(CreateEntityNotFoundException)} was null.");
			if (e == null) throw new ArgumentNullException(nameof(e), $"Found that provided inner exception in {nameof(CreateEntityNotFoundException)} was null.");

			throw new InvalidOperationException($"Failed to access {typeof(TReturnType).Name} from Entity: {guid}. Error: {e.Message}", e);
		}

		/// <summary>
		/// Retrieve the object of type <typeparamref name="TReturnType"/>
		/// from the entity mapped collection.
		/// </summary>
		/// <typeparam name="TReturnType">The entity mapped object.</typeparam>
		/// <param name="collection">The entity collection.</param>
		/// <param name="guid">The entity guid.</param>
		/// <exception cref="InvalidOperationException">Throws if the entity does not have data mapped to it.</exception>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TReturnType RetrieveEntity<TReturnType>([NotNull] this IReadOnlyDictionary<NetworkEntityGuid, TReturnType> collection, [NotNull] NetworkEntityGuid guid)
		{
			//No null checking because we hope to inline this
			try
			{
				return collection[guid];
			}
			catch(Exception e)
			{
				throw CreateEntityNotFoundException<TReturnType>(guid, e);
			}
		}
	}
}
