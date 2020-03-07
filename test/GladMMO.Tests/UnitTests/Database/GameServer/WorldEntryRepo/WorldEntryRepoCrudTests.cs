using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace GladMMO
{
	/// <summary>
	/// Runs all the crud repo interface default tests against the zone repository.
	/// </summary>
	[TestFixture]
	public class WorldEntryRepoCrudTests : GenericCrubRepositoryDefaultTests<ContentDatabaseContext, DatabaseBackedWorldEntryRepository, long, WorldEntryModel>
	{
		public override IEnumerable<long> TestCaseKeys => new long[] { 1, 2, 3, 5, 6, 7, 8, 9, 22, 6666, short.MaxValue, int.MaxValue };

		/// <inheritdoc />
		public override WorldEntryModel BuildRandomModel(bool generateKey)
		{
			return new WorldEntryModel(1, "127.0.0.1", Guid.NewGuid());
		}

		/// <inheritdoc />
		public override long ProduceKeyFromModel(WorldEntryModel model)
		{
			return model.WorldId;
		}
	}
}
