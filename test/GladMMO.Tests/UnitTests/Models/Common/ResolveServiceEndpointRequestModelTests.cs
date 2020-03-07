using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GladMMO
{
	[TestFixture]
	public static class ResolveServiceEndpointRequestTests
	{
		[Test]
		[TestCase((ClientRegionLocale)int.MaxValue, "test")]
		[TestCase((ClientRegionLocale)55555, "test")]
		[TestCase((ClientRegionLocale)(-5), "test")]
		[TestCase((ClientRegionLocale)(-5), "test")]
		public static void Test_Throws_On_Construction_With_Invalid_Argument_Region(ClientRegionLocale region, string service)
		{
			//assert
			Assert.Throws<ArgumentOutOfRangeException>(() => new ResolveServiceEndpointRequest(region, service));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN, "\t   ")]
		[TestCase(ClientRegionLocale.CN, null)]
		[TestCase(ClientRegionLocale.CN, "")]
		[TestCase(ClientRegionLocale.CN, "  ")]
		[TestCase(ClientRegionLocale.CN, null)]
		public static void Test_Throws_On_Construction_With_Invalid_Argument_Service(ClientRegionLocale region, string service)
		{
			//assert
			Assert.Throws<ArgumentException>(() => new ResolveServiceEndpointRequest(region, service));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN, "AuthenticationService")]
		[TestCase(ClientRegionLocale.EU, "AuthenticationService")]
		[TestCase(ClientRegionLocale.KR, "AuthenticationService")]
		[TestCase(ClientRegionLocale.RU, "AuthenticationService")]
		public static void Test_Doesnt_Throw_On_Valid_Arguments(ClientRegionLocale region, string service)
		{
			//assert
			Assert.DoesNotThrow(() => new ResolveServiceEndpointRequest(region, service));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN, "AuthenticationService")]
		[TestCase(ClientRegionLocale.EU, "AuthenticationService")]
		[TestCase(ClientRegionLocale.KR, "AuthenticationService")]
		[TestCase(ClientRegionLocale.RU, "AuthenticationService")]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(ClientRegionLocale region, string service)
		{
			//arrange
			ResolveServiceEndpointRequest model = new ResolveServiceEndpointRequest(region, service);

			//act
			string serializedModel = JsonConvert.SerializeObject(model);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
		}

		[Test]
		[TestCase(ClientRegionLocale.CN, "AuthenticationService")]
		[TestCase(ClientRegionLocale.EU, "AuthenticationService")]
		[TestCase(ClientRegionLocale.KR, "AuthenticationService")]
		[TestCase(ClientRegionLocale.RU, "AuthenticationService")]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(ClientRegionLocale region, string service)
		{
			//arrange
			ResolveServiceEndpointRequest model = new ResolveServiceEndpointRequest(region, service);

			//act
			ResolveServiceEndpointRequest deserializedModel =
				JsonConvert.DeserializeObject<ResolveServiceEndpointRequest>(JsonConvert.SerializeObject(model));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.True(Enum.IsDefined(typeof(ClientRegionLocale), deserializedModel.Region));
			Assert.NotNull(deserializedModel.ServiceType);
		}
	}
}