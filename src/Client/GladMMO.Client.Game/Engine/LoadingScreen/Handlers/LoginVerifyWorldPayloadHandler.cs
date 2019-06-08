using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	/*[AdditionalRegisterationAs(typeof(IServerRequestedSceneChangeEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class LoginVerifyWorldPayloadHandler : BaseGameClientGameMessageHandler<SMSG_LOGIN_VERIFY_WORLD_PAYLOAD>, IServerRequestedSceneChangeEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<ServerRequestedSceneChangeEventArgs> OnServerRequestedSceneChange;

		/// <inheritdoc />
		public LoginVerifyWorldPayloadHandler(ILog logger) 
			: base(logger)
		{
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_LOGIN_VERIFY_WORLD_PAYLOAD payload)
		{
			//When this packet is recieved it means a character is logging into the world
			//trinitycore sends this. I assume that WoW sends this so that the client could begin loading the proper ADT/terrain stuff
			//for the given map and location.

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Server Requested Map: {(int)payload.MapId}:{((PlayableGameScene)payload.MapId).ToString()}");

			//TODO: Use the map id.
			//So, we just broadcast that the scene has requested we change maps.
			OnServerRequestedSceneChange?.Invoke(this, new ServerRequestedSceneChangeEventArgs((PlayableGameScene)payload.MapId));

			return Task.CompletedTask;
		}
	}*/
}
