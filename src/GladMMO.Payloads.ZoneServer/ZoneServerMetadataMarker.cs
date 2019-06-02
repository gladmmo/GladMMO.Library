using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Class that offers high-level metadata about the payload assembly.
	/// </summary>
	public static class ZoneServerMetadataMarker
	{
		private static readonly Lazy<IReadOnlyDictionary<GamePayloadOperationCode, Type>> _ServerPayloadTypesByOpcode;
		private static readonly Lazy<IReadOnlyDictionary<GamePayloadOperationCode, Type>> _ClientPayloadTypesByOpcode;

		/// <summary>
		/// Collection of all Zone Server payload types.
		/// </summary>
		public static IReadOnlyCollection<Type> PayloadTypes { get; }


		/// <summary>
		/// Collection of all Zone server payload types as a dictionary
		/// with the key as the operation code.
		/// </summary>
		public static IReadOnlyDictionary<GamePayloadOperationCode, Type> ServerPayloadTypesByOpcode => _ServerPayloadTypesByOpcode.Value;

		/// <summary>
		/// Collection of all Zone client payload types as a dictionary
		/// with the key as the operation code.
		/// </summary>
		public static IReadOnlyDictionary<GamePayloadOperationCode, Type> ClientPayloadTypesByOpcode => _ClientPayloadTypesByOpcode.Value;

		public static IReadOnlyCollection<Type> AllProtobufModels { get; }

		static ZoneServerMetadataMarker()
		{
			PayloadTypes = typeof(ClientSessionClaimRequestPayload)
				.Assembly
				.GetExportedTypes()
				.Where(t => typeof(GameClientPacketPayload).IsAssignableFrom(t) || typeof(GameServerPacketPayload).IsAssignableFrom(t))
				.Distinct()
				.ToArray();

			_ServerPayloadTypesByOpcode = new Lazy<IReadOnlyDictionary<GamePayloadOperationCode, Type>>(() => PayloadTypes
				.Where(t => typeof(GameServerPacketPayload).IsAssignableFrom(t))
				.ToDictionary(type => type.GetCustomAttribute<GamePayloadAttribute>().OperationCode), true);

			_ClientPayloadTypesByOpcode = new Lazy<IReadOnlyDictionary<GamePayloadOperationCode, Type>>(() => PayloadTypes
				.Where(t => typeof(GameClientPacketPayload).IsAssignableFrom(t))
				.ToDictionary(type => type.GetCustomAttribute<GamePayloadAttribute>().OperationCode), true);

			AllProtobufModels = typeof(ClientSessionClaimRequestPayload)
				.Assembly
				.GetTypes()
				.Where(t => t.GetCustomAttribute<ProtoContractAttribute>() != null)
				.Concat(new Type[] { typeof(GameClientPacketPayload), typeof(GameServerPacketPayload) })
				.Distinct()
				.ToArray();
		}
	}
}
