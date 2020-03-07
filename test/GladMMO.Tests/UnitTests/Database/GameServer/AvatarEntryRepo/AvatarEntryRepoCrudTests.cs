using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace GladMMO
{
	/// <summary>
	/// Runs all the crud repo interface default tests against the avatar entry repo.
	/// </summary>
	[TestFixture]
	public class AvatarEntryRepoCrudTests : GenericCrubRepositoryDefaultTests<ContentDatabaseContext, DatabaseBackedAvatarEntryRepository, long, AvatarEntryModel>
	{
		public override IEnumerable<long> TestCaseKeys => new long[] { 1, 2, 3, 5, 6, 7, 8, 9, 22, 6666, short.MaxValue, int.MaxValue };

		/// <inheritdoc />
		public override AvatarEntryModel BuildRandomModel(bool generateKey)
		{
			return new AvatarEntryModel(1, "127.0.0.1", Guid.NewGuid());
		}

		/// <inheritdoc />
		public override long ProduceKeyFromModel(AvatarEntryModel model)
		{
			return model.AvatarId;
		}
	}
}
