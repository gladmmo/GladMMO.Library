using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GladMMO
{
	[JsonObject]
	public class AvatarPedestalInstanceModel : IGameObjectLinkable
	{
		//TODO: Combine the TargetGameObjectId and the LinkedGameObjectId. They are conceptually the same, legacy stuff.
		[JsonIgnore]
		public int LinkedGameObjectId => TargetGameObjectId;

		/// <summary>
		/// Defines the GameObject instance that
		/// this behavior is attached to. It is the primary
		/// and foreign key to the instance it's attached to.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		public int TargetGameObjectId { get; private set; }

		[JsonRequired]
		[JsonProperty]
		public int AvatarModelId { get; private set; }

		public AvatarPedestalInstanceModel(int targetGameObjectId, int avatarModelId)
		{
			if(targetGameObjectId <= 0) throw new ArgumentOutOfRangeException(nameof(targetGameObjectId));
			if(avatarModelId <= 0) throw new ArgumentOutOfRangeException(nameof(avatarModelId));

			TargetGameObjectId = targetGameObjectId;
			AvatarModelId = avatarModelId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		protected AvatarPedestalInstanceModel()
		{

		}
	}
}
