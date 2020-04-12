using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class LocalCharacterDataRepository : ILocalCharacterDataRepository
	{
		private readonly object SyncObj = new object();

		//We need this static because it should never change. The local ID should always be this ID
		//and any changes should reflect in any instance of this repository.
		private static ObjectGuid Id { get; set; }

		/// <inheritdoc />
		public ObjectGuid LocalCharacterGuid
		{
			get
			{
				lock(SyncObj)
					return Id;
			}
		}

		/// <inheritdoc />
		public void UpdateCharacterId(ObjectGuid characterId)
		{
			lock(SyncObj)
				//TODO: Check if it has changed, unload cache or something.
				Id = characterId;
		}
	}
}
