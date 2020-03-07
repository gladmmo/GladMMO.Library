using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class EntityVisibilityChangeContext : IEntityGuidContainer
	{
		/// <inheritdoc />
		public ObjectGuid EntityGuid { get; }

		public IReadonlyInterestCollection InterestCollection { get; }

		/// <inheritdoc />
		public EntityVisibilityChangeContext([NotNull] ObjectGuid entityGuid, [NotNull] IReadonlyInterestCollection interestCollection)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			InterestCollection = interestCollection ?? throw new ArgumentNullException(nameof(interestCollection));
		}
	}
}
