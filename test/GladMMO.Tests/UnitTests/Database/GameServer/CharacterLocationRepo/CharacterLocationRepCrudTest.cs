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
	public class CharacterLocationRepCrudTest : GenericCrubRepositoryDefaultTests<CharacterDatabaseContext, DatabaseBackedCharacterLocationRepository, int, CharacterLocationModel>
	{
		private static int CharacterIdIncrementable = 1;

		public override IEnumerable<int> TestCaseKeys => new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 22, 6666, short.MaxValue, int.MaxValue };

		/// <inheritdoc />
		public override CharacterLocationModel BuildRandomModel(bool generateKey)
		{
			int characterId = Interlocked.Increment(ref CharacterIdIncrementable);

			Random random = new Random();
			return new CharacterLocationModel(generateKey ? characterId : 0, (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), random.Next());
		}

		/// <inheritdoc />
		public override int ProduceKeyFromModel(CharacterLocationModel model)
		{
			return model.CharacterId;
		}
	}
}
