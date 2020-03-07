using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace GladMMO
{
	/// <summary>
	/// Runs all the crud repo interface default tests against the character repository.
	/// </summary>
	[TestFixture]
	public class CharacterRepoStringCrudTest : GenericCrubRepositoryDefaultTests<CharacterDatabaseContext, DatabaseBackedCharacterRepository, string, CharacterEntryModel>
	{
		private static int AccountIdIncrementable = 1;

		public override IEnumerable<string> TestCaseKeys => new string[] { "Andrew", "Lyle", "Shilo", "Sammy", "Skylar" };

		/// <inheritdoc />
		public override CharacterEntryModel BuildRandomModel(bool generateKey)
		{
			int accountid = Interlocked.Increment(ref AccountIdIncrementable);

			return new CharacterEntryModel(accountid, Guid.NewGuid().ToString());
		}

		/// <inheritdoc />
		public override string ProduceKeyFromModel(CharacterEntryModel model)
		{
			return model.CharacterName;
		}
	}
}
