using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class AvatarPedestalChangeResponse
	{
		/// <summary>
		/// This is the absolute backend truth about the entity's model id.
		/// This is the correct one, and can be verified against in-memory.
		/// </summary>
		[JsonProperty]
		public int PersistedModelId { get; private set; }

		public AvatarPedestalChangeResponse(int persistedModelId)
		{
			if (persistedModelId <= 0) throw new ArgumentOutOfRangeException(nameof(persistedModelId));

			PersistedModelId = persistedModelId;
		}
	}
}
