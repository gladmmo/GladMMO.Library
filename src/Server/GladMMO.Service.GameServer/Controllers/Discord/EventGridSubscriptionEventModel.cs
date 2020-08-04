using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// See: EventGrid event (copy pasted from there)
	/// </summary>
	[JsonObject]
	public sealed class EventGridSubscriptionEventModel
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "topic")]
		public string Topic { get; set; }

		[JsonProperty(PropertyName = "subject")]
		public string Subject { get; set; }

		[JsonProperty(PropertyName = "data")]
		public SubscriptionValidationEventData Data { get; set; }

		[JsonProperty(PropertyName = "eventType")]
		public string EventType { get; set; }

		[JsonProperty(PropertyName = "eventTime")]
		public DateTime EventTime { get; set; }

		[JsonProperty(PropertyName = "metadataVersion")]
		public string MetadataVersion { get; }

		[JsonProperty(PropertyName = "dataVersion")]
		public string DataVersion { get; set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		internal EventGridSubscriptionEventModel()
		{
			
		}
	}
}
