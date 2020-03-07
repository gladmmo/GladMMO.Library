using System; using FreecraftCore;
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

		[Headers("Cache-Control: max-age=5000")]
		[Get("/api/SpellEntry/levellearned")]
		Task<SpellLevelLearnedCollectionResponseModel> GetLevelLearnedSpellsAsync();

		[Headers("Cache-Control: max-age=5000")]
		[Get("/api/SpellEntry/levellearned/{class}")]
		Task<SpellLevelLearnedCollectionResponseModel> GetLevelLearnedSpellsAsync([AliasAs("class")] EntityPlayerClassType classType);

		[Headers("Cache-Control: max-age=5000")]
		[Get("/api/SpellEntry/levellearned/{class}/{level}")]
		Task<SpellLevelLearnedCollectionResponseModel> GetLevelLearnedSpellsAsync([AliasAs("class")] EntityPlayerClassType classType, [AliasAs("level")] int level);
	}
}
