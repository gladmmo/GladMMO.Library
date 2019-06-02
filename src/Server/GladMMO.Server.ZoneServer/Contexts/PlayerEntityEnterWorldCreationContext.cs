using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	public sealed class PlayerEntityEnterWorldCreationContext
	{
		public PlayerEntitySessionContext SessionContext { get; }

		public Vector3 SpawnPosition { get; }

		/// <inheritdoc />
		public PlayerEntityEnterWorldCreationContext([NotNull] PlayerEntitySessionContext sessionContext, Vector3 spawnPosition)
		{
			SessionContext = sessionContext ?? throw new ArgumentNullException(nameof(sessionContext));
			SpawnPosition = spawnPosition;
		}
	}
}
