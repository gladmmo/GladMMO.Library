using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using ProtoBuf.Meta;

namespace GladMMO
{
	public class ProtobufPayloadRegister
	{
		private readonly object SyncObj = new object();

		internal static bool isInitialized { get; private set; } = false;

		public ProtobufPayloadRegister()
		{
			ProtoBuf.Meta.RuntimeTypeModel.Default.AutoCompile = false;
		}

		public virtual void RegisterDefaults()
		{
			//Do nothing.
		}

		public void Register()
		{
			lock(SyncObj)
				if (isInitialized)
					return;

			lock (SyncObj)
			{
				//Double check
				if(isInitialized)
					return;

				RuntimeTypeModel.Default.Add(typeof(GameClientPacketPayload), true);
				RuntimeTypeModel.Default.Add(typeof(GameServerPacketPayload), true);

				RegisterSubType<GameClientPacketPayload>(ZoneServerMetadataMarker.ClientPayloadTypesByOpcode);
				RegisterSubType<GameServerPacketPayload>(ZoneServerMetadataMarker.ServerPayloadTypesByOpcode);

				isInitialized = true;
			}
		}

		private static void RegisterSubType<TBaseType>([NotNull] IReadOnlyDictionary<GamePayloadOperationCode, Type> payloadTypes)
		{
			if (payloadTypes == null) throw new ArgumentNullException(nameof(payloadTypes));

			foreach (var entry in payloadTypes)
			{
				UnityEngine.Debug.Log($"Registering: {entry.Value.Name} with Key: {entry.Key}");

				//TODO: Will this ever prevent a subtype registeration?
				RuntimeTypeModel.Default.Add(entry.Value, true);

				//TODO: Sometimes for unit testing this fails before the Protobuf model isn't reset. Figure a way to handle it. IfDefined breaks the Unity3D.
				RuntimeTypeModel.Default[typeof(TBaseType)]
					.AddSubType((int) entry.Key, entry.Value);
			}
		}
	}
}