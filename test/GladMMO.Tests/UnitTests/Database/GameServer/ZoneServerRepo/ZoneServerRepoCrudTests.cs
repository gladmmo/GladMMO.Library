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
	public class ZoneServerRepoCrudTests : GenericCrubRepositoryDefaultTests<CharacterDatabaseContext, DatabaseBackedZoneServerRepository, int, ZoneInstanceEntryModel>
	{
		public override IEnumerable<int> TestCaseKeys => new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 22, 6666, short.MaxValue, int.MaxValue };

		private int incrementalKey = 1;

		/// <inheritdoc />
		public override ZoneInstanceEntryModel BuildRandomModel(bool generateKey)
		{
			return new ZoneInstanceEntryModel(Interlocked.Increment(ref incrementalKey), "127.0.0.1", 5080, 1);
		}

		/// <inheritdoc />
		public override int ProduceKeyFromModel(ZoneInstanceEntryModel model)
		{
			return model.ZoneId;
		}
	}
}
