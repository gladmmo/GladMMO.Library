using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	public abstract class AbstractMetadataMarker<TTypeToScanAssemblyFrom>
		where TTypeToScanAssemblyFrom : class
	{
		/// <summary>
		/// The models defined in the assembly.
		/// </summary>
		public static IEnumerable<Type> ModelTypes { get; }

		public static IEnumerable<Type> AllTypes { get; }

		static AbstractMetadataMarker()
		{
			AllTypes = typeof(TTypeToScanAssemblyFrom)
				.GetTypeInfo()
				.Assembly
				.DefinedTypes
				.Select(t => t.AsType());

			ModelTypes = AllTypes
				.Where(t => t.GetCustomAttribute<JsonObjectAttribute>() != null);
		}
	}
}
