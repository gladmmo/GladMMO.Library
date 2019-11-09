using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	public sealed class WorldActorState : IEntityActorStateContainable
	{
		//Kinda a hack, but we can use entity data container for World too maybe.
		public IEntityDataFieldContainer EntityData { get; } = new EntityFieldDataCollection(8);

		//TODO: Eventually we should create a world entity guid.
		public NetworkEntityGuid EntityGuid { get; } = NetworkEntityGuid.Empty;

		/// <summary>
		/// Factory for actor creation for the world.
		/// </summary>
		public IActorRefFactory WorldActorFactory { get; internal set; }

		/// <summary>
		/// The world death watch service.
		/// </summary>
		public ICanWatch DeathWatchService { get; internal set; }
	}
}
