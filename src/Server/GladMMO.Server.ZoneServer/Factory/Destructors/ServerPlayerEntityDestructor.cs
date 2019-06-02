using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	public sealed class ServerPlayerEntityDestructor : IObjectDestructorable<PlayerEntityDestructionContext>
	{
		private IObjectDestructorable<NetworkEntityGuid> EntityDestructor { get; }

		private IReadonlyEntityGuidMappable<GameObject> GuidToGameObjectMap { get; }

		private IReadonlyEntityGuidMappable<InterestCollection> InterestCollections { get; }

		private IEntityInterestChangeEventSpoofable InterestEventSpoofer { get; }

		/// <inheritdoc />
		public ServerPlayerEntityDestructor(
			[NotNull] IObjectDestructorable<NetworkEntityGuid> entityDestructor, 
			[NotNull] IReadonlyEntityGuidMappable<GameObject> guidToGameObjectMap,
			[NotNull] IReadonlyEntityGuidMappable<InterestCollection> interestCollections,
			[NotNull] IEntityInterestChangeEventSpoofable interestEventSpoofer)
		{
			EntityDestructor = entityDestructor ?? throw new ArgumentNullException(nameof(entityDestructor));
			GuidToGameObjectMap = guidToGameObjectMap ?? throw new ArgumentNullException(nameof(guidToGameObjectMap));
			InterestCollections = interestCollections ?? throw new ArgumentNullException(nameof(interestCollections));
			InterestEventSpoofer = interestEventSpoofer ?? throw new ArgumentNullException(nameof(interestEventSpoofer));
		}

		/// <inheritdoc />
		public bool Destroy(PlayerEntityDestructionContext obj)
		{
			bool result = EntityDestructor.Destroy(obj.EntityGuid);

			if(result)
			{
				//To avoid major issues with previous physics based issue we're writing this horriblely slow, hacky solution\
				//This actually scales O(n) which isn't bad. A quick iteration and a hashmap check basically.
				foreach(var ic in InterestCollections)
				{
					if(ic.Value.Contains(obj.EntityGuid))
					{
						//We just spoof an exit to every interested collection who knows of the entity being cleaned up.
						InterestEventSpoofer.SpoofExitInterest(new EntityInterestChangeEventArgs(ic.Key, obj.EntityGuid, EntityInterestChangeEventArgs.ChangeType.Exit));
					}
				}
			}

			return result;
		}

		/// <inheritdoc />
		public bool OwnsEntityToDestruct(int connectionId)
		{
			throw new NotSupportedException($"TODO: We shouldn't have the destructor actually check this.");
		}
	}
}
