using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public static class ResolveServiceEndpointResponseTests
	{
		[Test]
		[TestCase((ResolveServiceEndpointResponseCode)int.MaxValue)]
		[TestCase((ResolveServiceEndpointResponseCode)55555)]
		[TestCase((ResolveServiceEndpointResponseCode)(-5))]
		public static void Test_Throws_On_Construction_With_Invalid_Argument_ResponseCode(ResolveServiceEndpointResponseCode value)
		{
			//assert
			Assert.Throws<ArgumentOutOfRangeException>(() => new ResolveServiceEndpointResponse(value));
		}

		[Test]
		public static void Test_Throws_On_Construction_With_Invalid_Argument_Endpoint()
		{
			//assert
			Assert.Throws<ArgumentNullException>(() => new ResolveServiceEndpointResponse(null));
		}

		[Test]
		[TestCase("AuthenticationService")]
		public static void Test_Doesnt_Throw_On_Valid_Arguments(string serviveType)
		{
			//assert
			Assert.DoesNotThrow(() => new ResolveServiceEndpointResponse(new ResolvedEndpoint("test", 55)));
			Assert.DoesNotThrow(() => new ResolveServiceEndpointResponse(ResolveServiceEndpointResponseCode.Success));
		}

		[Test]
		[TestCase(ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		[TestCase(ResolveServiceEndpointResponseCode.GeneralRequestError)]
		[TestCase(ResolveServiceEndpointResponseCode.ServiceUnlisted)]

		public static void Test_isSuccessful_False_On_Failed_ResponseCodes(ResolveServiceEndpointResponseCode resultCode)
		{
			//arrange
			ResolveServiceEndpointResponse model = new ResolveServiceEndpointResponse(resultCode);

			//assert
			Assert.False(model.isSuccessful);
		}

		[Test]
		[TestCase("127.0.0.1", 55)]
		[TestCase("www.domain.com", 80)]
		public static void Test_isSuccessful_True_On_Success(string endpoint, int port)
		{
			//arrange
			ResolveServiceEndpointResponse model = new ResolveServiceEndpointResponse(new ResolvedEndpoint(endpoint, port));

			//assert
			Assert.True(model.isSuccessful);
		}

		[Test]
		[TestCase("127.0.0.1", 55)]
		[TestCase("www.domain.com", 80)]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(string endpoint, int port)
		{
			//arrange
			ResolveServiceEndpointResponse authModel = new ResolveServiceEndpointResponse(new ResolvedEndpoint(endpoint, port));

			//act
			string serializedModel = JsonConvert.SerializeObject(authModel);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(endpoint));
			Assert.True(serializedModel.Contains(port.ToString()));
		}

		[Test]
		[TestCase("127.0.0.1", 55)]
		[TestCase("www.domain.com", 80)]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(string endpoint, int port)
		{
			//arrange
			ResolveServiceEndpointResponse authModel = new ResolveServiceEndpointResponse(new ResolvedEndpoint(endpoint, port));

			//act
			ResolveServiceEndpointResponse deserializedModel =
				JsonConvert.DeserializeObject<ResolveServiceEndpointResponse>(JsonConvert.SerializeObject(authModel));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.NotNull(deserializedModel.Endpoint);
			Assert.NotNull(deserializedModel.Endpoint.Address);
			Assert.AreEqual(endpoint, deserializedModel.Endpoint.Address);
			Assert.AreEqual(port, deserializedModel.Endpoint.Port);
		}

		[Test]
		[TestCase(ResolveServiceEndpointResponseCode.Success)]
		[TestCase(ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		[TestCase(ResolveServiceEndpointResponseCode.ServiceUnlisted)]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(ResolveServiceEndpointResponseCode resultCode)
		{
			//arrange
			ResolveServiceEndpointResponse authModel = new ResolveServiceEndpointResponse(resultCode);

			//act
			string serializedModel = JsonConvert.SerializeObject(authModel);

			//assert
			Assert.NotNull(serializedModel);
			Assert.True(!serializedModel.Contains(nameof(authModel.isSuccessful)), $"JSON modle contains what should be unlisted field {nameof(authModel.isSuccessful)}. JSON: {serializedModel}");
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(((int)resultCode).ToString()));
		}

		[Test]
		[TestCase(ResolveServiceEndpointResponseCode.Success)]
		[TestCase(ResolveServiceEndpointResponseCode.ServiceUnlisted)]
		[TestCase(ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(ResolveServiceEndpointResponseCode resultCode)
		{
			//arrange
			ResolveServiceEndpointResponse authModel = new ResolveServiceEndpointResponse(resultCode);

			//act
			ResolveServiceEndpointResponse deserializedModel =
				JsonConvert.DeserializeObject<ResolveServiceEndpointResponse>(JsonConvert.SerializeObject(authModel));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.Null(deserializedModel.Endpoint);
			Assert.True(Enum.IsDefined(typeof(ResolveServiceEndpointResponseCode), deserializedModel.ResultCode), $"Enum value: {deserializedModel.ResultCode} was not valid.");
		}
	}
}