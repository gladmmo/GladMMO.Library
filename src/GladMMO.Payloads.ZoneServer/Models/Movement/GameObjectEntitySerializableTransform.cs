using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using UnityEngine;

namespace GladMMO
{
	//TODO: We should find a way to compress/reduce the data of this chunk.
	/// <summary>
	/// The entity position and rotation.
	/// </summary>
	[ProtoContract]
	public sealed class GameObjectEntitySerializableTransform
	{
		/// <summary>
		/// The position of the entity.
		/// </summary>
		[ProtoMember(1)]
		public Vector3 Position { get; private set; }

		/// <summary>
		/// The rotation of the object.
		/// </summary>
		[ProtoMember(2)]
		public Vector3 EulerRotation { get; private set; }

		/// <inheritdoc />
		public GameObjectEntitySerializableTransform(Vector3 position, Vector3 eulerRotation)
		{
			Position = position;
			EulerRotation = eulerRotation;
		}

		public GameObjectEntitySerializableTransform(Transform entityTransform)
			: this(entityTransform.position, entityTransform.eulerAngles)
		{

		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private GameObjectEntitySerializableTransform()
		{
			
		}
	}
}
