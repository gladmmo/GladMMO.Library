using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerNpcEntryModel
	{
		/// <summary>
		/// The network GUID associated with the entry.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		public NetworkEntityGuid Guid { get; private set; }

		/// <summary>
		/// The NPC template id of the entry.
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

		//TODO: Consolidate into own model
		[JsonRequired]
		[JsonProperty]
		public NpcMovementType Movement { get; private set; }

		[JsonRequired]
		[JsonProperty]
		public int MovementData { get; private set; }

		/// <inheritdoc />
		public ZoneServerNpcEntryModel([NotNull] NetworkEntityGuid guid, int templateId, Vector3 initialPosition, NpcMovementType movement, int movementData)
		{
			if(templateId <= 0) throw new ArgumentOutOfRangeException(nameof(templateId));

			Guid = guid ?? throw new ArgumentNullException(nameof(guid));
			TemplateId = templateId;
			InitialPosition = initialPosition;
			Movement = movement;
			MovementData = movementData;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ZoneServerNpcEntryModel()
		{
			
		}
	}
}
