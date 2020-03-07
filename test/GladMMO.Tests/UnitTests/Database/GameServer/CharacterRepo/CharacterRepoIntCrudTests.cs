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
	public class CharacterRepoIntCrudTests : GenericCrubRepositoryDefaultTests<CharacterDatabaseContext, DatabaseBackedCharacterRepository, int, CharacterEntryModel>
	{
		private static int AccountIdIncrementable = 1;

		public override IEnumerable<int> TestCaseKeys => new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 22, 6666, short.MaxValue, int.MaxValue };

		/// <inheritdoc />
		public override CharacterEntryModel BuildRandomModel(bool generateKey)
		{
			int accountid = Interlocked.Increment(ref AccountIdIncrementable);

			return new CharacterEntryModel(accountid, Guid.NewGuid().ToString());
		}

		/// <inheritdoc />
		public override int ProduceKeyFromModel(CharacterEntryModel model)
		{
			return model.CharacterId;
		}
	}
}
