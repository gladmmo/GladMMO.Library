using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public static class ClientDataCollectionExtensions
	{
		internal static Dictionary<Type, IReadOnlyDictionary<uint, string>> InternalStringReferenceMap { get; } = new Dictionary<Type, IReadOnlyDictionary<uint, string>>();

		public static string GetString<T>(this StringDBCReference<T> stringReference)
		{
			IReadOnlyDictionary<uint, string> references = InternalStringReferenceMap[typeof(T)];

			if (references.ContainsKey(stringReference.StringReferenceOffset))
				return references[stringReference.StringReferenceOffset];

			return $"UNKNOWN_DBC_STRING_{typeof(T).Name}";
		}
	}
}
