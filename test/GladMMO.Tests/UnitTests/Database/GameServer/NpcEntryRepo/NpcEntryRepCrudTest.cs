using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GladMMO.Database;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Reinterpret.Net;

namespace GladMMO
{
	/// <summary>
	/// Runs all the crud repo interface default tests against the NPC Entry Repository.
	/// </summary>
	[TestFixture]
	public class NpcEntryRepCrudTest : GenericCrubRepositoryDefaultTests<NpcDatabaseContext, DatabaseBackedNpcEntryRepository, int, NPCEntryModel>
	{
		public override IEnumerable<int> TestCaseKeys => new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 22, 6666, short.MaxValue, int.MaxValue };

		/// <inheritdoc />
		public override NPCEntryModel BuildRandomModel(bool generateKey)
		{
			Random random = new Random();

			//TODO: IF we ever have a map table we may need to redo the Random map (might fail FK?)
			return new NPCEntryModel(random.Next(), new Vector3<float>(NextRandomFloat(random), NextRandomFloat(random), NextRandomFloat(random)), NextRandomFloat(random), random.Next(), NpcMovementType.Stationary, 1);
		}

		private static float NextRandomFloat(Random random)
		{
			return random.Next().Reinterpret().Reinterpret<float>();
		}

		/// <inheritdoc />
		public override int ProduceKeyFromModel(NPCEntryModel model)
		{
			return model.EntryId;
		}

		[Test]
		[TestCase(55)]
		[TestCase(56)]
		[TestCase(12)]
		[TestCase(13)]
		[TestCase(105)]
		public async Task Test_RetrieveEntriesByMapId_Returns_All_Entries_With_MapId(int mapId)
		{
			//arrange
			INpcEntryRepository entryRepo = BuildEmptyRepository();

			//act
			await entryRepo.TryCreateAsync(new NPCEntryModel(1, new Vector3<float>(5, 6, 7), 55, mapId, NpcMovementType.Stationary, 1));
			await entryRepo.TryCreateAsync(new NPCEntryModel(2, new Vector3<float>(5, 6, 7), 55, mapId, NpcMovementType.Stationary, 1));
			await entryRepo.TryCreateAsync(new NPCEntryModel(3, new Vector3<float>(5, 6, 7), 55, mapId, NpcMovementType.Stationary, 1));

			IReadOnlyCollection<NPCEntryModel> entryModels = await entryRepo.RetrieveAllWithMapIdAsync(mapId);

			//assert
			Assert.AreEqual(3, entryModels.Count);
			Assert.False(entryModels.Any(e => e.MapId != mapId));
		}

		[Test]
		[TestCase(55)]
		[TestCase(56)]
		[TestCase(12)]
		[TestCase(13)]
		[TestCase(105)]
		public async Task Test_RetrieveEntriesByMapId_Doesnt_Return_Entries_With_Different_MapId(int mapId)
		{
			int unavailableMapId = 5;

			if(mapId == unavailableMapId)
			{
				Assert.Inconclusive($"Cannot use {nameof(mapId)}: {unavailableMapId}");
				return;
			}

			//arrange
			INpcEntryRepository entryRepo = BuildEmptyRepository();

			//act
			await entryRepo.TryCreateAsync(new NPCEntryModel(1, new Vector3<float>(5, 6, 7), 55, mapId, NpcMovementType.Stationary, 1));
			await entryRepo.TryCreateAsync(new NPCEntryModel(2, new Vector3<float>(5, 6, 7), 55, mapId, NpcMovementType.Stationary, 1));
			await entryRepo.TryCreateAsync(new NPCEntryModel(3, new Vector3<float>(5, 6, 7), 55, mapId, NpcMovementType.Stationary, 1));

			await entryRepo.TryCreateAsync(new NPCEntryModel(3, new Vector3<float>(5, 6, 7), 55, unavailableMapId, NpcMovementType.Stationary, 1));
			await entryRepo.TryCreateAsync(new NPCEntryModel(3, new Vector3<float>(5, 6, 7), 55, unavailableMapId, NpcMovementType.Stationary, 1));

			IReadOnlyCollection<NPCEntryModel> entryModels = await entryRepo.RetrieveAllWithMapIdAsync(mapId);

			//assert
			Assert.AreEqual(3, entryModels.Count);
			Assert.False(entryModels.Any(e => e.MapId != mapId));
		}

		public static INpcEntryRepository BuildEmptyRepository()
		{
			ServiceCollection collection = new ServiceCollection();

			IServiceProvider provider = collection.AddTestDatabaseContext<NpcDatabaseContext>()
				.AddDefaultDataTestServices()
				.AddTransient<INpcEntryRepository, DatabaseBackedNpcEntryRepository>()
				.BuildServiceProvider();

			return provider.GetService<INpcEntryRepository>();
		}
	}
}