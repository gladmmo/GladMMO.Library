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
	public class CharacterFriendRelationshipsCrudTests : GenericCrubRepositoryDefaultTests<CharacterDatabaseContext, DatabaseBackedCharacterFriendRelationshipRepository, int, CharacterFriendRelationshipModel>
	{
		public override IEnumerable<int> TestCaseKeys => new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 22, 6666, short.MaxValue, int.MaxValue };

		/// <inheritdoc />
		public override CharacterFriendRelationshipModel BuildRandomModel(bool generateKey)
		{
			Random random = new Random((int)DateTime.UtcNow.Ticks);

			return new CharacterFriendRelationshipModel(random.Next(), random.Next());
		}

		/// <inheritdoc />
		public override int ProduceKeyFromModel(CharacterFriendRelationshipModel model)
		{
			return model.FriendshipRelationshipId;
		}
	}
}
