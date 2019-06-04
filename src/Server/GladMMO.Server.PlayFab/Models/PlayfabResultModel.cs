using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class PlayFabResultModel<TResponseObjectType>
	{
		[JsonProperty(PropertyName = "code")]
		public HttpStatusCode ResultCode { get; private set; }

		[JsonProperty(PropertyName = "status")]
		public string ResultStatus { get; private set; }

		[JsonProperty(PropertyName = "data")]
		public TResponseObjectType Data { get; private set; }

		public PlayFabResultModel(TResponseObjectType data)
		{
			Data = data;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public PlayFabResultModel()
		{
			
		}
	}
}
