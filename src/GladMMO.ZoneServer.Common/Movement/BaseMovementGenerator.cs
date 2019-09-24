using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	public abstract class LateInitializationBaseMovementGenerator<TDataInputType> : MoveGenerator
		where TDataInputType : class, IMovementData
	{
		/// <summary>
		/// The movement data used by this generator.
		/// </summary>
		protected TDataInputType MovementData { get; private set; }

		/// <summary>
		/// If overriden, make sure to call base.
		/// Otherwise <see cref="InitializeMovementData"/> will not be
		/// called and <see cref="MovementData"/> was remain null.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="currentTime"></param>
		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			MovementData = InitializeMovementData(entity, currentTime);
			return entity.transform.position;
		}

		protected abstract TDataInputType InitializeMovementData(GameObject entity, long currentTime);
	}

	public abstract class MoveGenerator : IMovementGenerator<GameObject>
	{
		public Vector3 CurrentPosition { get; private set; }

		public bool isRunning { get; private set; } = false;

		protected abstract Vector3 Start(GameObject entity, long currentTime);

		/// <inheritdoc />
		public void Update(GameObject entity, long currentTime)
		{
			if (!isRunning)
			{
				CurrentPosition = Start(entity, currentTime);
				isRunning = true;
			}

			CurrentPosition = InternalUpdate(entity, currentTime); //don't update if we called Start
		}

		/// <summary>
		/// Called on <see cref="Update"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="currentTime"></param>
		protected abstract Vector3 InternalUpdate(GameObject entity, long currentTime);
	}

	/// <summary>
	/// Base for movement generators that control client and serverside movement simulation.
	/// </summary>
	/// <typeparam name="TDataInputType">The data input type.</typeparam>
	public abstract class BaseMovementGenerator<TDataInputType> : MoveGenerator
		where TDataInputType : class, IMovementData
	{
		/// <summary>
		/// The movement data used by this generator.
		/// </summary>
		protected TDataInputType MovementData { get; }

		/// <inheritdoc />
		protected BaseMovementGenerator([NotNull] TDataInputType movementData)
		{
			MovementData = movementData ?? throw new ArgumentNullException(nameof(movementData));
		}
	}
}