using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class GameObjectTemplateModel : IObjectTemplateModel
	{
		/// <summary>
		/// The GameObject's template id.
		/// </summary>
		[JsonProperty]
		public int TemplateId { get; private set; }

		/// <summary>
		/// The ID of the GameObject's model.
		/// </summary>
		[JsonProperty]
		public long ModelId { get; private set; }

		/// <summary>
		/// The name of the GameObject. Will be the one the client sees, the one the NameQuery will return.
		/// </summary>
		[JsonProperty]
		public string GameObjectName { get; private set; }

		[JsonProperty]
		public GameObjectType ObjectType { get; private set; }

		public GameObjectTemplateModel(int gameObjectTemplateId, long modelId, [NotNull] string gameObjectName, GameObjectType objectType)
		{
			if (modelId <= 0) throw new ArgumentOutOfRangeException(nameof(modelId));
			if (string.IsNullOrWhiteSpace(gameObjectName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(gameObjectName));
			if (gameObjectTemplateId <= 0) throw new ArgumentOutOfRangeException(nameof(gameObjectTemplateId));
			if (!Enum.IsDefined(typeof(GameObjectType), objectType)) throw new InvalidEnumArgumentException(nameof(objectType), (int) objectType, typeof(GameObjectType));

			TemplateId = gameObjectTemplateId;
			ModelId = modelId;
			GameObjectName = gameObjectName;
			ObjectType = objectType;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private GameObjectTemplateModel()
		{
			
		}
	}
}
