using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public sealed class CharacterRepositoryTests
	{
		[Test]
		public static void ShouldAlwaysPass()
		{
			Assert.Pass();
		}

		[Test]
		public static void Test_MockedDatabaseContext_Can_Build()
		{
			//arange
			ServiceCollection collection = new ServiceCollection();

			IServiceProvider provider = collection.AddTestDatabaseContext<CharacterDatabaseContext>()
				.AddDefaultDataTestServices()
				.BuildServiceProvider();

			//act
			CharacterDatabaseContext result = provider.GetService<CharacterDatabaseContext>();

			//assert
			Assert.NotNull(result);
		}

		//We keep this because it actually tests retrieve name
		[Test]
		[TestCase("Andrew")]
		[TestCase("Lyle")]
		[TestCase("Shilo")]
		[TestCase("Sammy")]
		[TestCase("Skylar")]
		public static async Task Test_Retrieve_Name_Works_On_Added_Models(string name)
		{
			//arrange
			ICharacterRepository repository = BuildEmptyRepository();

			//act
			await repository.TryCreateAsync(new CharacterEntryModel(1, name));

			string result = await repository.RetrieveNameAsync(1);

			//assert
			Assert.AreEqual(name, result);
		}

		[Test]
		[TestCase("Andrew", "Lyle", "Shilo", "Sammy", "Skylar")]
		public static async Task Test_Retrieve_Characters_By_Account_Id_Works(params string[] names)
		{
			//arrange
			ICharacterRepository repository = BuildEmptyRepository();

			//act: Add all to the same account key
			foreach(string n in names)
				await repository.TryCreateAsync(new CharacterEntryModel(1, n));

			//Add another not associated with the first id
			await repository.TryCreateAsync(new CharacterEntryModel(2, Guid.NewGuid().ToString()));

			int[] result = await repository.CharacterIdsForAccountId(1);

			//assert
			Assert.AreEqual(names.Length, result.Length, $"Expected character id collection length to be the same count as names.");
			foreach(int key in result)
				Assert.True(names.Contains((await repository.RetrieveAsync(key)).CharacterName));
		}

		public static ICharacterRepository BuildEmptyRepository()
		{
			ServiceCollection collection = new ServiceCollection();

			IServiceProvider provider = collection.AddTestDatabaseContext<CharacterDatabaseContext>()
				.AddDefaultDataTestServices()
				.AddTransient<ICharacterRepository, DatabaseBackedCharacterRepository>()
				.BuildServiceProvider();

			return provider.GetService<ICharacterRepository>();
		}
	}
}
