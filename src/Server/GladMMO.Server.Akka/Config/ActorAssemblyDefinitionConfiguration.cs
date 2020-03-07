using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public class ActorAssemblyDefinitionConfiguration
	{
		[JsonRequired]
		[JsonProperty]
		public string[] AssemblyNames { get; }

		public ActorAssemblyDefinitionConfiguration(string[] assemblyNames)
		{
			AssemblyNames = assemblyNames ?? throw new ArgumentNullException(nameof(assemblyNames));
			AssemblyNames = assemblyNames.Select(s => s.ToLower()).ToArray();

			if (!AssemblyNames.Contains("GladMMO.Server.ZoneServer".ToLower()))
				AssemblyNames = assemblyNames.ToList().Concat(new string[] {"GladMMO.Server.ZoneServer".ToLower()}).ToArray();
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public ActorAssemblyDefinitionConfiguration()
		{

		}
	}
}
