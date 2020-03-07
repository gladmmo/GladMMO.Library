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
	public static class PlayerAccountJWTModelTests
	{
		[Test]
		[TestCase(null)]
		[TestCase("")]
		[TestCase("      ")]
		public static void Test_Throws_On_Construction_With_Invalid_AccessToken(string accessToken)
		{
			//assert
			Assert.Throws<ArgumentException>(() => new PlayerAccountJWTModel(accessToken));
		}

		[Test]
		[TestCase(null, null)]
		[TestCase("Error", null)]
		[TestCase(null, "ErrorDef")]
		[TestCase(null, "")]
		[TestCase("", null)]
		[TestCase(null, "  ")]
		[TestCase("  ", null)]
		[TestCase("Error", "  ")]
		[TestCase("  ", "ErrorDef")]
		[TestCase("   ", "   ")]
		[TestCase("", "   ")]
		[TestCase("   ", "")]
		public static void Test_Throws_On_Construction_With_Invalid_ErrorArgs(string error, string errorDefinition)
		{
			//assert
			Assert.Throws<ArgumentException>(() => new PlayerAccountJWTModel(error, errorDefinition));
		}

		[Test]
		[TestCase("Test")]
		[TestCase(@"346346346TtstydsdYTttadtdt494(F(JSF(Js(&^^*#$")]
		public static void Test_Doesnt_Throw_On_Valid_AccessToken(string accessToken)
		{
			//assert
			Assert.DoesNotThrow(() => new PlayerAccountJWTModel(accessToken));
		}

		[Test]
		[TestCase("Error", "ErrorDef")]
		[TestCase("Blahblahblah", "Something terrible wrong so bad it crashes the whole backend.")]
		public static void Test_Doesnt_Throw_On_Valid_ErrorArgs(string error, string errorDefinition)
		{
			//assert
			Assert.DoesNotThrow(() => new PlayerAccountJWTModel(error, errorDefinition));
		}

		[Test]
		[TestCase("Error", "ErrorDef")]
		[TestCase("Blahblahblah", "Something terrible wrong so bad it crashes the whole backend.")]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace_ErrorArgs(string error, string errorDefinition)
		{
			//arrange
			PlayerAccountJWTModel model = new PlayerAccountJWTModel(error, errorDefinition);

			//act
			string serializedModel = JsonConvert.SerializeObject(model);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(error));
			Assert.True(serializedModel.Contains(errorDefinition));
		}

		[Test]
		[TestCase("Error", "ErrorDef")]
		[TestCase("Blahblahblah", "Something terrible wrong so bad it crashes the whole backend.")]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_ErrorArgs(string error, string errorDefinition)
		{
			//arrange
			PlayerAccountJWTModel model = new PlayerAccountJWTModel(error, errorDefinition);

			//act
			PlayerAccountJWTModel deserializedModel = JsonConvert.DeserializeObject<PlayerAccountJWTModel>(JsonConvert.SerializeObject(model));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.NotNull(deserializedModel.Error);
			Assert.NotNull(deserializedModel.ErrorDescription);
			Assert.AreEqual(error, deserializedModel.Error);
			Assert.AreEqual(errorDefinition, deserializedModel.ErrorDescription);
		}

		[Test]
		[TestCase("Token")]
		[TestCase(@"3468468nh***ADHAD8hd8dghsad*DD*SY")]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace_AccessToken(string accessToken)
		{
			//arrange
			PlayerAccountJWTModel model = new PlayerAccountJWTModel(accessToken);

			//act
			string serializedModel = JsonConvert.SerializeObject(model);

			//assert
			Assert.NotNull(serializedModel);
			Assert.True(!serializedModel.Contains(nameof(model.isTokenValid)), $"JSON modle contains what should be unlisted field {nameof(model.isTokenValid)}. JSON: {serializedModel}");
			Assert.True(!serializedModel.Contains("_isTokenValid"), $"JSON modle contains what should be unlisted field _isTokenValid. JSON: {serializedModel}");
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(accessToken));
		}

		[Test]
		[TestCase("Token")]
		[TestCase(@"8insdf89snhdf89h*(H*(hdf89HFSHs8hfsf8h*h@*@*$H$")]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_AccessToken(string accessToken)
		{
			//arrange
			PlayerAccountJWTModel model = new PlayerAccountJWTModel(accessToken);

			//act
			PlayerAccountJWTModel deserializedModel = JsonConvert.DeserializeObject<PlayerAccountJWTModel>(JsonConvert.SerializeObject(model));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.NotNull(deserializedModel.AccessToken);
			Assert.IsNotEmpty(deserializedModel.AccessToken);
			Assert.AreEqual(accessToken, deserializedModel.AccessToken);
		}

		[Test]
		[TestCase("Token")]
		[TestCase(@"8insdf89snhdf89h*(H*(hdf89HFSHs8hfsf8h*h@*@*$H$")]
		public static void Test_PlayerAccountJWTModel_Indicates_IsValid_When_AccessToken_IsPresent(string accessToken)
		{
			//arrange
			PlayerAccountJWTModel model = new PlayerAccountJWTModel(accessToken);

			//act
			PlayerAccountJWTModel deserializedModel = JsonConvert.DeserializeObject<PlayerAccountJWTModel>(JsonConvert.SerializeObject(model));

			//assert
			Assert.IsTrue(deserializedModel.isTokenValid);
		}

		[Test]
		[TestCase("Error", "ErrorDef")]
		[TestCase("Blahblahblah", "Something terrible wrong so bad it crashes the whole backend.")]
		public static void Test_PlayerAccountJWTModel_Indicates_IsValid_When_Error_Is_Present(string error, string errorDescription)
		{
			//arrange
			PlayerAccountJWTModel model = new PlayerAccountJWTModel(error, errorDescription);

			//act
			PlayerAccountJWTModel deserializedModel = JsonConvert.DeserializeObject<PlayerAccountJWTModel>(JsonConvert.SerializeObject(model));

			//assert
			Assert.IsFalse(deserializedModel.isTokenValid);
		}
	}
}
