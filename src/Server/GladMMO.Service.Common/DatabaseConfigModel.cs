using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class DatabaseConfigModel
	{
		/// <summary>
		/// The connection string for the database.
		/// </summary>
		[JsonProperty]
		public string ConnectionString { get; set; }

		public DatabaseConfigModel()
		{
			
		}
	}
}
