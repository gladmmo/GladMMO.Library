using System; using FreecraftCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Generic.Math;
using GladMMO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public abstract class GenericCrubRepositoryDefaultTests<TDbContextType, TRepositoryType, TKeyType, TModelType>
		where TDbContextType : DbContext
		where TRepositoryType : class, IGenericRepositoryCrudable<TKeyType, TModelType>
	{
		private class MarkerType { }

		/// <summary>
		/// Implementers should provide a set of test keys to use for testing.
		/// DO NOT REFERENCE THIS IN TEST ATTRIBUTES.
		/// </summary>
		public abstract IEnumerable<TKeyType> TestCaseKeys { get; }

		//This is quite the clever hack. What it does is it allows us to use the abstract instance collection
		//in the child type while meeting the requirement of the source being a static
		static GenericCrubRepositoryDefaultTests()
		{
			//TODO: This will only work if there is only one implementation that matches
			//The static ctor is promsied to run for every closed generic type
			//See: https://stackoverflow.com/questions/2936580/c-sharp-generic-static-constructor?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
			//Now we know the declaring type. The closed generic type.
			//Therefore we can find a Type that matches it in the assembly
			//we can then create it and get the test cases.
			Type childType = typeof(MarkerType)
				.Assembly
				.GetTypes()
				.First(t => !t.IsAbstract && typeof(GenericCrubRepositoryDefaultTests<TDbContextType, TRepositoryType, TKeyType, TModelType>).IsAssignableFrom(t));

			var childObj = Activator.CreateInstance(childType)
				as GenericCrubRepositoryDefaultTests<TDbContextType, TRepositoryType, TKeyType, TModelType>;

			TestKeysSource = childObj.TestCaseKeys;
		}
		
		public static IEnumerable<TKeyType> TestKeysSource { get; private set; }

		public GenericCrubRepositoryDefaultTests()
		{
			
		}

		[Test]
		[TestCaseSource(nameof(TestKeysSource))]
		public virtual async Task Test_Contains_Id_False_On_Empty_Repository(TKeyType key)
		{
			//arrange
			IGenericRepositoryCrudable<TKeyType, TModelType> repository = BuildEmptyRepository();

			TKeyType keyValue = key;

			//act
			bool result = await repository.ContainsAsync(keyValue);

			//assert
			Assert.False(result, $"Expected empty repository with {nameof(IGenericRepositoryCrudable<TKeyType, TModelType>.ContainsAsync)} to produce false on empy.");
		}

		[Test]
		public virtual async Task Test_Update_Replaces_Existing_Model()
		{
			//arrange
			IGenericRepositoryCrudable<TKeyType, TModelType> repository = BuildEmptyRepository();
			TModelType model1 = BuildRandomModel(true);
			await repository.TryCreateAsync(model1)
				.ConfigureAwaitFalse();

			TModelType model2 = BuildRandomModel(false);

			//act
			await repository.UpdateAsync(this.ProduceKeyFromModel(model1), model2)
				.ConfigureAwaitFalseVoid();
			bool containsModel1 = await repository.ContainsAsync(ProduceKeyFromModel(model1))
				.ConfigureAwaitFalse();

			TModelType model3 = await repository.RetrieveAsync(ProduceKeyFromModel(model1))
				.ConfigureAwaitFalse();

			//assert
			//Model1 should still seem like its in the database, but it should be Model2.
			Assert.True(containsModel1, $"Model1 key was in the database.");
			Assert.AreEqual(ProduceKeyFromModel(model1), ProduceKeyFromModel(model3));
		}

		[Test]
		public async Task Test_Can_Add_New_Model_To_Repository()
		{
			//arrange
			IGenericRepositoryCrudable<TKeyType, TModelType> repository = BuildEmptyRepository();

			TModelType model = BuildRandomModel(true);

			//act
			bool addResult = await repository.TryCreateAsync(model);

			//assert
			Assert.True(addResult, $"Could not add Model: {model.ToString()} to repository for some reason.");
		}

		[Test]
		public async Task Test_Adding_Duplicate_Model_Throws_Exception_With_Exeption_Mentioning_Key()
		{
			//arrange
			IGenericRepositoryCrudable<TKeyType, TModelType> repository = BuildEmptyRepository();

			TModelType model = BuildRandomModel(true);

			//act
			bool addResult = await repository.TryCreateAsync(model);

			//assert
			Assert.ThrowsAsync<ArgumentException>(async () => await repository.TryCreateAsync(model));

			//Check message value
			try
			{
				await repository.TryCreateAsync(model);
			}
			catch(Exception e)
			{
				Assert.True(e.Message.ToLower().Contains("key"), $"Exception methods should contain the word key somewhere in their message. Was: {e.Message}");
			}
		}

		[Test]
		public async Task Test_Can_Add_Two_Unique_Models_To_Repository()
		{
			//arrange
			IGenericRepositoryCrudable<TKeyType, TModelType> repository = BuildEmptyRepository();

			TModelType model1 = BuildRandomModel(true);
			TModelType model2 = BuildRandomModel(true);

			//act
			bool addResult1 = await repository.TryCreateAsync(model1);
			bool addResult2 = await repository.TryCreateAsync(model2);

			//assert
			Assert.True(addResult1, $"Could not add Model1: {model1.ToString()} to repository for some reason.");
			Assert.True(addResult2, $"Could not add Model1: {model2.ToString()} to repository for some reason.");
		}

		[Test]
		public async Task Test_Contains_Works_When_Unique_Models_Are_Added([Range(1, 10)] int count)
		{
			//arrange
			IGenericRepositoryCrudable<TKeyType, TModelType> repository = BuildEmptyRepository();
			Dictionary<TKeyType, TModelType> models = await BuildTestModelDictionary(count, repository);

			//assert
			foreach(var kvp in models)
			{
				bool result = await repository.ContainsAsync(kvp.Key);
				Assert.True(result, $"Could not find Key: {kvp.Key.ToString()} Model: {kvp.Value.ToString()} in the repository for some reason.");
			}
		}

		private async Task<Dictionary<TKeyType, TModelType>> BuildTestModelDictionary(int count, IGenericRepositoryCrudable<TKeyType, TModelType> repository)
		{
			Dictionary<TKeyType, TModelType> models = new Dictionary<TKeyType, TModelType>();

			for(int i = 0; i < count; i++)
			{
				TModelType model = BuildRandomModel(true);
				await repository.TryCreateAsync(model);
				models[ProduceKeyFromModel(model)] = model;
			}

			return models;
		}

		[Test]
		public async Task Test_Retreieve_Produces_Correct_Result([Range(1, 10)] int count)
		{
			//arrange
			IGenericRepositoryCrudable<TKeyType, TModelType> repository = BuildEmptyRepository();
			Dictionary<TKeyType, TModelType> models = await BuildTestModelDictionary(count, repository);

			//assert
			foreach(var kvp in models)
			{
				TModelType result = await repository.RetrieveAsync(kvp.Key);

				//We need to check that equality AND key values are the same
				Assert.AreEqual(ProduceKeyFromModel(result), kvp.Key, $"Retrieve produced model with non-matching keys.");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <exception cref="InconclusiveException">Throws if the key cannot be constructed.</exception>
		/// <returns></returns>
		private static TKeyType TryBuildKey(int key)
		{
			//TODO: Is there a better way to do this?
			TKeyType keyValue = default(TKeyType);
			try
			{
				keyValue = GenericMath.Convert<int, TKeyType>(key);
			}
			catch(Exception)
			{
				Assert.Inconclusive($"Unable to convert Key: {key} to Type: {typeof(TKeyType).Name} so skipping.");
			}

			return keyValue;
		}

		/// <summary>
		/// Should build a random model for use for the testing class.
		/// </summary>
		/// <returns></returns>
		public abstract TModelType BuildRandomModel(bool generateKey);

		public abstract TKeyType ProduceKeyFromModel(TModelType model);

		public static IGenericRepositoryCrudable<TKeyType, TModelType> BuildEmptyRepository()
		{
			ServiceCollection collection = new ServiceCollection();

			IServiceProvider provider = collection
				.AddTestDatabaseContext<TDbContextType>()
				.AddDefaultDataTestServices()
				.AddTransient<IGenericRepositoryCrudable<TKeyType, TModelType>, TRepositoryType>()
				.BuildServiceProvider();

			return provider.GetService<IGenericRepositoryCrudable<TKeyType, TModelType>>();
		}
	}
}
