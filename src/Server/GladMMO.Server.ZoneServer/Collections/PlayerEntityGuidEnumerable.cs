using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	public interface IPlayerEntityGuidEnumerable : IEnumerable<NetworkEntityGuid>
	{

	}

	public sealed class PlayerEntityGuidEnumerable : IPlayerEntityGuidEnumerable
	{
		private IReadonlyKnownEntitySet EntitySet { get; }

		public PlayerEntityGuidEnumerable([NotNull] IReadonlyKnownEntitySet entitySet)
		{
			EntitySet = entitySet ?? throw new ArgumentNullException(nameof(entitySet));
		}

		/// <inheritdoc />
		public IEnumerator<NetworkEntityGuid> GetEnumerator()
		{
			foreach(var entity in EntitySet)
			{
				//If the session with the ID does not have an entity associated with it then a player
				//for the session is not in the world
				if (entity.EntityType != EntityType.Player)
					continue;

				yield return entity;
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
