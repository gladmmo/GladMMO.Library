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
		protected override void Start(GameObject entity, long currentTime)
		{
			MovementData = InitializeMovementData(entity, currentTime);
		}

		protected abstract TDataInputType InitializeMovementData(GameObject entity, long currentTime);
	}

	public abstract class MoveGenerator : IMovementGenerator<GameObject>
	{
		protected bool hasStartFired { get; private set; } = false;

		protected abstract void Start(GameObject entity, long currentTime);

		/// <inheritdoc />
		public void Update(GameObject entity, long currentTime)
		{
			if (!hasStartFired)
			{
				Start(entity, currentTime);
				hasStartFired = true;
			}
			else
				InternalUpdate(entity, currentTime); //don't update if we called Start
		}

		/// <summary>
		/// Called on <see cref="Update"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="currentTime"></param>
		protected abstract void InternalUpdate(GameObject entity, long currentTime);
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