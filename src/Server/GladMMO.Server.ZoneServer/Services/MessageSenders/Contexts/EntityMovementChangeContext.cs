﻿using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class EntityMovementMessageContext : IEntityGuidContainer
	{
		/// <summary>
		/// The entity guid of the connection a movement
		/// message/update should be sent to.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityMovementMessageContext([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
