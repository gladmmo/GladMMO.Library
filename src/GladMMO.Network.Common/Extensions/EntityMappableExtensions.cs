using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GladMMO
{
	public static class EntityMappableExtensions
	{
		/// <summary>
		/// Replaces an existing Entity with key <see cref="guid"/> with the value <see cref="obj"/>
		/// if it already exists. Throws if the data does not exist.
		/// </summary>
		/// <typeparam name="TReturnType">The type of object to add.</typeparam>
		/// <param name="collection">The entity collection.</param>
		/// <param name="guid">The entity guid.</param>
		/// <param name="obj">The object to add.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ReplaceObject<TReturnType>([NotNull] this IDictionary<NetworkEntityGuid, TReturnType> collection, [NotNull] NetworkEntityGuid guid, TReturnType obj)
			where TReturnType : class
		{
			//No null checking because we hope to inline this.
			try
			{
				//We strictly enforce that the entity be known/existing in this component collection.
				if(collection.ContainsKey(guid))
					collection[guid] = obj; //Replaces the existing object.
				else
					throw CreateEntityDoesNotExistException<TReturnType>(guid);
			}
			catch(Exception e)
			{
				throw CreateEntityCollectionException<TReturnType>(guid, e);
			}
		}

		/// <summary>
		/// Adds the entity object with the provided key <see cref="guid"/> if it doesn't exists.
		/// If the key already exists it will throw.
		/// </summary>
		/// <typeparam name="TReturnType">The type of object to add.</typeparam>
		/// <param name="collection">The entity collection.</param>
		/// <param name="guid">The entity guid.</param>
		/// <param name="obj">The object to add.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddObject<TReturnType>([NotNull] this IDictionary<NetworkEntityGuid, TReturnType> collection, [NotNull] NetworkEntityGuid guid, TReturnType obj)
			where TReturnType : class
		{
			//No null checking because we hope to inline this.
			try
			{
				collection.Add(guid, obj); //Does NOT replace the entity if there is one.
			}
			catch(Exception e)
			{
				throw CreateEntityCollectionException<TReturnType>(guid, e);
			}
		}

		private static Exception CreateEntityDoesNotExistException<TReturnType>(NetworkEntityGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid), $"Found that provided entity guid in {nameof(CreateEntityCollectionException)} was null.");

			return new InvalidOperationException($"Entity does not exist in Collection {typeof(TReturnType).Name} from Entity: {guid}.");
		}

		private static Exception CreateEntityCollectionException<TReturnType>(NetworkEntityGuid guid, Exception e)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid), $"Found that provided entity guid in {nameof(CreateEntityCollectionException)} was null.");
			if (e == null) throw new ArgumentNullException(nameof(e), $"Found that provided inner exception in {nameof(CreateEntityCollectionException)} was null.");

			return new InvalidOperationException($"Failed to access {typeof(TReturnType).Name} from Entity: {guid}. Error: {e.Message}", e);
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
				throw CreateEntityCollectionException<TReturnType>(guid, e);
			}
		}
	}
}
