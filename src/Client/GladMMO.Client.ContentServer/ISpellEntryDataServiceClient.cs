﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	[Headers("User-Agent: SDK 0.0.1")]
	public interface ISpellEntryDataServiceClient
	{
		/// <summary>
		/// Endpoint that provides the default spell data.
		/// </summary>
		/// <returns>Collection of the default spell data.</returns>
		[Headers("Cache-Control: max-age=5000")]
		[Get("/api/SpellEntry/default")]
		Task<SpellDefinitionCollectionResponseModel> GetDefaultSpellDataAsync();
	}
}
