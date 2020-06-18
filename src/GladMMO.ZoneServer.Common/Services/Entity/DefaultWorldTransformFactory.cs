using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Simplified interface for <see cref="IFactoryCreatable{TCreateType,TContextType}"/>
	/// </summary>
	public interface IWorldTransformFactory : IFactoryCreatable<WorldTransform, MovementBlockData>, IFactoryCreatable<WorldTransform, ObjectGuid>
	{
		
	}

	public sealed class DefaultWorldTransformFactory : IWorldTransformFactory, IFactoryCreatable<WorldTransform, MovementBlockData>
	{
		//Only used for when they provide only a guid.
		private IReadonlyEntityGuidMappable<MovementBlockData> MovementBlockMappable { get; }

		public DefaultWorldTransformFactory([NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementBlockMappable)
		{
			MovementBlockMappable = movementBlockMappable ?? throw new ArgumentNullException(nameof(movementBlockMappable));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns>Can return null.</returns>
		public WorldTransform Create([NotNull] MovementBlockData context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			if (context.IsLiving)
			{
				MovementInfo moveInfo = context.MoveInfo;
				Vector3 position = moveInfo.Position.ToUnityVector();
				float orientation = moveInfo.Orientation.ToUnity3DYAxisRotation();

				return new WorldTransform(position.x, position.y, position.z, orientation);
			}
			else if(context.IsStationaryObject)
			{
				//GameObjects can have this type of movement flags set
				//if they don't move.
				StationaryMovementInfo stationaryMovementInfo = context.StationaryObjectMovementInformation;
				Vector3 position = stationaryMovementInfo.Position.ToUnityVector();
				float orientation = stationaryMovementInfo.Orientation.ToUnity3DYAxisRotation();

				return new WorldTransform(position.x, position.y, position.z, orientation);
			}
			else if (context.HasUpdatePosition && context.IsDead) //HasUpdatePosition and IsStationaryObject is mutually exclusive
			{
				//TODO: Check if actually a corpse, GameObjects and Corpses share this. But this is actually fine to be honest, due to weird format of CorpseInfo
				CorpseInfo corpseMoveInfo = context.DeadMovementInformation;
				Vector3 position = corpseMoveInfo.GoLocation.ToUnityVector();
				float orientation = corpseMoveInfo.Orientation.ToUnity3DYAxisRotation();

				return new WorldTransform(position.x, position.y, position.z, orientation);
			}
			else
				return null;
		}

		public WorldTransform Create([NotNull] ObjectGuid context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			return Create(MovementBlockMappable.RetrieveEntity(context));
		}
	}
}
