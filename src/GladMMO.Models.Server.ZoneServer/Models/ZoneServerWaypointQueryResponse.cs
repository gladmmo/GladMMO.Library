using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerWaypointQueryResponse : IResponseModel<WaypointQueryResponseCode>, ISucceedable
	{
		/// <summary>
		/// The collection of <see cref="Vector3"/> waypoints with
		/// the requested id.
		/// </summary>
		[JsonRequired]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = nameof(Waypoints))]
		[JsonConverter(typeof(Vector3ArrayConverter))]
		private Vector3[] _Waypoints;

		[JsonIgnore]
		public IReadOnlyCollection<Vector3> Waypoints => _Waypoints ?? Array.Empty<Vector3>();

		/// <inheritdoc />
		[JsonProperty]
		public WaypointQueryResponseCode ResultCode { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == WaypointQueryResponseCode.Success;

		/// <inheritdoc />
		public ZoneServerWaypointQueryResponse([NotNull] Vector3[] waypoints)
		{
			_Waypoints = waypoints ?? throw new ArgumentNullException(nameof(waypoints));
			ResultCode = WaypointQueryResponseCode.Success;
		}

		/// <inheritdoc />
		public ZoneServerWaypointQueryResponse(WaypointQueryResponseCode resultCode)
		{
			//TODO: Check and make sure it's not success, because if it is we need actual waypoint data.
			if(!Enum.IsDefined(typeof(WaypointQueryResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(WaypointQueryResponseCode));
			ResultCode = resultCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private ZoneServerWaypointQueryResponse()
		{
			
		}
	}
}
