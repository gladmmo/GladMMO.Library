using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	//See: https://docs.microsoft.com/en-us/azure/event-grid/post-to-custom-topic
	//See: https://docs.microsoft.com/en-us/azure/event-grid/post-to-custom-topic
	[JsonObject]
	public sealed class AzureEventGridRequest<TEventType>
		where TEventType : class
	{
		/// <summary>
		/// Unique identifier for the event.
		/// </summary>
		[JsonProperty(PropertyName = "id")]
		public string Id { get; private set; } = Guid.NewGuid().ToString();

		[JsonProperty(PropertyName = "data")]
		public TEventType Data { get; private set; }

		[JsonProperty(PropertyName = "dataVersion")]
		public string DataVersion { get; private set; } = "1.0";

		[JsonProperty(PropertyName = "eventTime")]
		public string EventTime { get; private set; } = DateTime.UtcNow.ToString();

		/// <summary>
		/// One of the registered event types for this event source.
		/// </summary>
		[JsonProperty(PropertyName = "eventType")]
		public string EventType { get; private set; }

		/// <summary>
		/// Publisher-defined path to the event subject.
		/// </summary>
		[JsonProperty(PropertyName = "subject")]
		public string Subject { get; private set; }

		public AzureEventGridRequest(TEventType data, string eventType, string subject)
		{
			Data = data ?? throw new ArgumentNullException(nameof(data));
			EventType = eventType ?? throw new ArgumentNullException(nameof(eventType));
			Subject = subject ?? throw new ArgumentNullException(nameof(subject));
		}

		[JsonConstructor]
		internal AzureEventGridRequest()
		{
			
		}
	}
}
