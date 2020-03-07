using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace GladMMO
{
	[JsonObject]
	public sealed class GameObjectInstanceModel : IInstanceObjectModel
	{
		/// <summary>
		/// The network GUID associated with the entry.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		public ObjectGuid Guid { get; private set; }

		/// <summary>
		/// The GameObject template id of the entry.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		public int TemplateId { get; private set; }

		/// <summary>
		/// The initial spawn position for the entry.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		[JsonConverter(typeof(Vector3Converter))] //TODO: Make custom attribute
		public Vector3 InitialPosition { get; private set; }

		/// <summary>
		/// Creature's initial rotation around the Y-Axis.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		public float YAxisRotation { get; private set; }

		/// <inheritdoc />
		public GameObjectInstanceModel([NotNull] ObjectGuid guid, int templateId, Vector3 initialPosition, float yRotation)
		{
			if(templateId <= 0) throw new ArgumentOutOfRangeException(nameof(templateId));

			Guid = guid ?? throw new ArgumentNullException(nameof(guid));
			TemplateId = templateId;
			InitialPosition = initialPosition;
			YAxisRotation = yRotation;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private GameObjectInstanceModel()
		{
			
		}
	}
}
