using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ProtoBuf.Meta;

namespace GladMMO
{
	public class ProtobufPayloadRegister
	{
		public ProtobufPayloadRegister()
		{
		}

		public virtual void RegisterDefaults()
		{
			//Do nothing.
		}

		public void Register()
		{
			//HelloKitty: So, we've already run the registeration of this. So we should not continue.
			if (RuntimeTypeModel.Default.IsDefined(typeof(GameServerPacketPayload)))
				return;

			RuntimeTypeModel.Default.AutoCompile = false;

			RuntimeTypeModel.Default.Add(typeof(GameClientPacketPayload), true);
			RuntimeTypeModel.Default.Add(typeof(GameServerPacketPayload), true);

			RegisterSubType<GameClientPacketPayload>(ZoneServerMetadataMarker.ClientPayloadTypesByOpcode);
			RegisterSubType<GameServerPacketPayload>(ZoneServerMetadataMarker.ServerPayloadTypesByOpcode);
		}

		private static void RegisterSubType<TBaseType>([NotNull] IReadOnlyDictionary<GamePayloadOperationCode, Type> payloadTypes)
		{
			if (payloadTypes == null) throw new ArgumentNullException(nameof(payloadTypes));

			foreach (var entry in payloadTypes)
			{
				//TODO: Will this ever prevent a subtype registeration?
				RuntimeTypeModel.Default.Add(entry.Value, true);

				//TODO: Sometimes for unit testing this fails before the Protobuf model isn't reset. Figure a way to handle it. IfDefined breaks the Unity3D.
				RuntimeTypeModel.Default[typeof(TBaseType)]
					.AddSubType((int) entry.Key, entry.Value);
			}
		}
	}
}