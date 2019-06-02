using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ProtoBuf.Meta;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Unity3D version of <see cref="ProtobufPayloadRegister"/>.
	/// Provides a method <see cref="RegisterDefaults"/>
	/// </summary>
	public sealed class Unity3DProtobufPayloadRegister : ProtobufPayloadRegister
	{
		public Unity3DProtobufPayloadRegister()
		{
		}

		/// <summary>
		/// Registers the default Unity3D types with Protobuf.
		/// </summary>
		public override void RegisterDefaults()
		{
			base.RegisterDefaults();

			//Register Vector3
			if (!RuntimeTypeModel.Default.IsDefined(typeof(Vector3)))
				RuntimeTypeModel.Default.Add(typeof(Vector3), false)
					.Add(nameof(Vector3.x), nameof(Vector3.y), nameof(Vector3.z));

			//Register Vector2
			if (!RuntimeTypeModel.Default.IsDefined(typeof(Vector2)))
				RuntimeTypeModel.Default.Add(typeof(Vector2), false)
					.Add(nameof(Vector2.x), nameof(Vector2.y));
		}
	}
}