using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GladMMO.Database;
using NUnit.Framework;

namespace GladMMO
{
	/// <summary>
	/// Runs all the crud repo interface default tests against the character repository.
	/// </summary>
	[TestFixture]
	public class WaypointsRepoCrudTest : GenericCrubRepositoryDefaultTests<CommonGameDatabaseContext, DatabaseBackedWaypointsRepository, PathWaypointKey, PathWaypointModel>
	{
		private static int PathIdIncrementable = 1;

		private static int PointIdIncrementable = 1;

		public override IEnumerable<PathWaypointKey> TestCaseKeys => 
			new PathWaypointKey[] { new PathWaypointKey(1,2), new PathWaypointKey(1, 1), new PathWaypointKey(1, 3), new PathWaypointKey(2, 1), new PathWaypointKey(2, 2), new PathWaypointKey(2, 3) };

		/// <inheritdoc />
		public override PathWaypointModel BuildRandomModel(bool generateKey)
		{
			int pathId = Interlocked.Increment(ref PathIdIncrementable);
			int pointId = Interlocked.Increment(ref PointIdIncrementable);

			Random random = new Random();
			return new PathWaypointModel(generateKey ? pathId : 0, generateKey ? pointId : 0, new GladMMO.Database.Vector3<float>((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()));
		}

		/// <inheritdoc />
		public override PathWaypointKey ProduceKeyFromModel(PathWaypointModel model)
		{
			return new PathWaypointKey(model.PathId, model.PointId);
		}

		[Test]
		public override async Task Test_Update_Replaces_Existing_Model()
		{
			Assert.Warn($"Cannot currently test Update for {nameof(PathWaypointModel)}. We should try it some other way.");
		}
	}
}
