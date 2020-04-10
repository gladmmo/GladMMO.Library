using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public interface IClientDataCollectionContainer
	{
		IKeyedClientDataCollection<MapEntry<StringDBCReference<MapEntry<string>>>> MapEntry { get; }
	}
}
