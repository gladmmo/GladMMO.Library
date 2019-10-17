using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class LocalCharacterDataRepository : ILocalCharacterDataRepository
	{
		private readonly object SyncObj = new object();

		//We need this static because it should never change. The local ID should always be this ID
		//and any changes should reflect in any instance of this repository.
		private static int Id { get; set; }

		/// <inheritdoc />
		public int CharacterId
		{
			get
			{
				lock(SyncObj)
					return Id;
			}
		}

		/// <inheritdoc />
		public void UpdateCharacterId(int characterId)
		{
			lock(SyncObj)
				//TODO: Check if it has changed, unload cache or something.
				Id = characterId;
		}
	}
}
