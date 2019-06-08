using System;
using System.Collections.Generic;
using System.Text;
using GladMMO;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Response to <see cref="ResolveServiceEndpointRequest"/> containing the endpoint of the requested service.
	/// </summary>
	[JsonObject]
	public sealed class ResolveServiceEndpointResponse : IResponseModel<ResolveServiceEndpointResponseCode>, ISucceedable
	{
		/// <summary>
		/// The resolved endpoint.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)] //can be null if no endpoint is available
		public ResolvedEndpoint Endpoint { get; private set; } //JSON requires it to have a setter for some reason

		/// <inheritdoc />
		[JsonProperty]
		[JsonRequired]
		public ResolveServiceEndpointResponseCode ResultCode { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == ResolveServiceEndpointResponseCode.Success;

		public ResolveServiceEndpointResponse(ResolvedEndpoint endpoint)
		{
			if(endpoint == null) throw new ArgumentNullException(nameof(endpoint));

			//We allow null on the endpoint but not if they pass this DTO one
			Endpoint = endpoint;

			//We can assume success since they provided an endpoint
			ResultCode = ResolveServiceEndpointResponseCode.Success;
		}

		public ResolveServiceEndpointResponse(ResolveServiceEndpointResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(ResolveServiceEndpointResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the ResolveServiceEndpointResponseCode enum.");

			ResultCode = resultCode;
		}

		/// <summary>
		/// Protected serializer ctor.
		/// </summary>
		protected ResolveServiceEndpointResponse()
		{

		}

		public override string ToString()
		{
			if (isSuccessful)
				return $"Result: {ResultCode} Endpoint: {Endpoint}";
			else
				return $"Result: {ResultCode}";
		}
	}
}