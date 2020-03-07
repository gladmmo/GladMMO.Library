using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Payload sent by the client to update the server about local client
	/// movement data.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.LoadNewScene)]
	public sealed class LoadNewSceneEventPayload : GameServerPacketPayload
	{
		/// <summary>
		/// The new scene that the client should load.
		/// </summary>
		[ProtoMember(1)]
		public PlayableGameScene SceneToLoad { get; private set; }

		/// <inheritdoc />
		public LoadNewSceneEventPayload(PlayableGameScene sceneToLoad)
		{
			if(!Enum.IsDefined(typeof(PlayableGameScene), sceneToLoad)) throw new InvalidEnumArgumentException(nameof(sceneToLoad), (int)sceneToLoad, typeof(PlayableGameScene));

			SceneToLoad = sceneToLoad;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected LoadNewSceneEventPayload()
		{
			
		}
	}
}
