using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all client locales.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ClientRegionLocale
	{
		/// <summary>
		/// United States
		/// </summary>
		US = 1,

		/// <summary>
		/// Europe
		/// </summary>
		EU,

		/// <summary>
		/// China
		/// </summary>
		CN,

		/// <summary>
		/// Korea
		/// </summary>
		KR,

		/// <summary>
		/// Russia
		/// </summary>
		RU
	}
}