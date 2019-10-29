using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerRegistrationResponse
	{
		//TODO: Should the zone registry service indicate anything to the zone?

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public ZoneServerRegistrationResponse()
		{
			
		}
	}
}
