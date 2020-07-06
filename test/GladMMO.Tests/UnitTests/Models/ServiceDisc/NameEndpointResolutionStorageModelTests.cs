using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public static class NameEndpointResolutionStorageModelTests
	{
		[Test]
		[TestCase((ClientRegionLocale)int.MaxValue)]
		[TestCase((ClientRegionLocale)55555)]
		[TestCase((ClientRegionLocale)(-5))]
		public static void Test_Throws_On_Construction_With_Invalid_Argument_ServiceType(ClientRegionLocale value)
		{
			//assert
			Assert.Throws<ArgumentOutOfRangeException>(() => new NameEndpointResolutionStorageModel(value, new Dictionary<string, ResolvedEndpoint>()));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN)]
		[TestCase(ClientRegionLocale.EU)]
		[TestCase(ClientRegionLocale.KR)]
		[TestCase(ClientRegionLocale.RU)]
		public static void Test_Throws_On_Construction_With_Invalid_Argument_Dictionary(ClientRegionLocale value)
		{
			//assert
			Assert.Throws<ArgumentNullException>(() => new NameEndpointResolutionStorageModel(value, null));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN)]
		[TestCase(ClientRegionLocale.EU)]
		[TestCase(ClientRegionLocale.KR)]
		[TestCase(ClientRegionLocale.RU)]
		public static void Test_Doesnt_Throw_On_Valid_Arguments(ClientRegionLocale value)
		{
			//assert
			Assert.DoesNotThrow(() => new NameEndpointResolutionStorageModel(value, new Dictionary<string, ResolvedEndpoint>()));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN)]
		[TestCase(ClientRegionLocale.EU)]
		[TestCase(ClientRegionLocale.KR)]
		[TestCase(ClientRegionLocale.RU)]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(ClientRegionLocale value)
		{
			//arrange
			Dictionary<string, ResolvedEndpoint> endpoints = new Dictionary<string, ResolvedEndpoint>()
			{
				{"AuthenticationService", new ResolvedEndpoint("127.0.0.1", 5555)}
			};
			NameEndpointResolutionStorageModel model = new NameEndpointResolutionStorageModel(value, endpoints);

			//act
			string serializedModel = JsonConvert.SerializeObject(model);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(endpoints.Values.First().Address));
			Assert.True(serializedModel.Contains(nameof(model.ServiceEndpoints)));
			Assert.True(serializedModel.Contains(endpoints.Values.First().Port.ToString()));
			Assert.True(serializedModel.Contains(value.ToString()));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN)]
		[TestCase(ClientRegionLocale.EU)]
		[TestCase(ClientRegionLocale.KR)]
		[TestCase(ClientRegionLocale.RU)]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(ClientRegionLocale value)
		{
			//arrange
			Dictionary<string, ResolvedEndpoint> endpoints = new Dictionary<string, ResolvedEndpoint>()
			{
				{"AuthenticationService", new ResolvedEndpoint("127.0.0.1", 5555)}
			};
			NameEndpointResolutionStorageModel model = new NameEndpointResolutionStorageModel(value, endpoints);

			//act
			NameEndpointResolutionStorageModel deserializedModel =
				JsonConvert.DeserializeObject<NameEndpointResolutionStorageModel>(JsonConvert.SerializeObject(model));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.NotNull(deserializedModel.ServiceEndpoints);
			Assert.IsNotEmpty(deserializedModel.ServiceEndpoints);
			Assert.True(Enum.IsDefined(typeof(ClientRegionLocale), deserializedModel.Region));
			Assert.NotNull(deserializedModel.ServiceEndpoints.Keys.First());
			Assert.AreEqual(endpoints.First().Key, deserializedModel.ServiceEndpoints.First().Key);
			Assert.AreEqual(endpoints.First().Value.Address, deserializedModel.ServiceEndpoints.First().Value.Address);
		}
	}
}