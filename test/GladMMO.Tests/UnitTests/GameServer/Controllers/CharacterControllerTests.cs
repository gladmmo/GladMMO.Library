using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Moq;
using NUnit.Framework;
using static GladMMO.ControllerTestsHelpers;

namespace GladMMO
{
	[TestFixture]
	public class CharacterControllerTests
	{
		[Test]
		public void Test_Can_Build_Mocked_Controller()
		{
			//arrange
			CharacterController controller = BuildCharacterController<CharacterController>();

			//assert
			Assert.NotNull(controller);
		}

		[Test]
		public async Task Test_Get_Characters_Empty_Datastore_Produces_Empty_Character_Result()
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<CharacterController>("Test", 1);
			CharacterController controller = serviceProvider.GetService<CharacterController>();

			//act
			CharacterListResponse response = await controller.GetCharacters();

			//assert
			Assert.NotNull(response);
			Assert.NotNull(response.CharacterIds);
			Assert.True(response.CharacterIds.Count == 0);
			Assert.True(response.ResultCode == CharacterListResponseCode.NoCharactersFoundError);
			Assert.False(response.isSuccessful);
		}

		[Test]
		public async Task Test_Get_Characters_Contains_Same_Size_As_Characters_Added([Range(1, 20)] int count)
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<CharacterController>("Test", 1);
			CharacterController controller = serviceProvider.GetService<CharacterController>();

			List<string> names = await AddTestValuesToRepository(count, serviceProvider);

			//act
			CharacterListResponse response = await controller.GetCharacters();

			//assert
			Assert.AreEqual(count, response.CharacterIds.Count);
			Assert.True(response.ResultCode == CharacterListResponseCode.Success);
			Assert.True(response.isSuccessful);
		}

		//This test is incredibly important. We do not want to ever spit out characters from another account somehow
		[Test]
		public async Task Test_Get_Characters_Contains_No_Characters_Added_From_Other_Account([Range(1, 20)] int count)
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<CharacterController>("Test", 1);
			CharacterController controller = serviceProvider.GetService<CharacterController>();

			List<string> names = await AddTestValuesToRepository(count, serviceProvider, 2);

			//act
			CharacterListResponse response = await controller.GetCharacters();

			//assert
			Assert.AreEqual(0, response.CharacterIds.Count, $"Expected NO characters to be provided. They're all under a different account. This is CRITICAL!");
			Assert.True(response.ResultCode == CharacterListResponseCode.NoCharactersFoundError);
			Assert.False(response.isSuccessful);
		}

		/*[Test]
		public async Task Test_Can_Get_Name_From_Character_Id([Range(1, 20)] int count)
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<NameQueryController>("Test", 1);
			NameQueryController controller = serviceProvider.GetService<NameQueryController>();

			List<string> names = await AddTestValuesToRepository(count, serviceProvider, 2);

			List<string> resultQueryNames = new List<string>(count);

			//assert
			for(int i = 1; i < count + 1; i++)
			{
				NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
					.WithId(i)
					.WithType(EntityType.Player)
					.Build();

				NameQueryResponse nameQueryResponse = ControllerTestsHelpers.GetActionResultObject<NameQueryResponse>(await controller.NameQuery(guid.RawGuidValue));
				Assert.True(nameQueryResponse.isSuccessful);
				Assert.NotNull(nameQueryResponse.EntityName);
				resultQueryNames.Add(nameQueryResponse.EntityName);
			}

			//Check that each inserted name is available in the total query results
			foreach(string s in names)
				Assert.True(resultQueryNames.Contains(s));
		}

		[Test]
		[TestCase(short.MaxValue)]
		[TestCase(-1)]
		[TestCase(int.MaxValue)]
		[TestCase(int.MaxValue - 1)]
		public async Task Test_Can_Not_NameQuery_Unknown_Ids(int value)
		{
			ulong keyToCheck = (ulong)value;
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<NameQueryController>("Test", 1);
			NameQueryController controller = serviceProvider.GetService<NameQueryController>();
			List<string> names = await AddTestValuesToRepository(20, serviceProvider, 2);

			//act
			NameQueryResponse result = ControllerTestsHelpers.GetActionResultObject<NameQueryResponse>(await controller.NameQuery(keyToCheck));

			//assert
			Assert.False(result.isSuccessful);
			Assert.True(result.ResultCode == NameQueryResponseCode.UnknownIdError);
			Assert.True(String.IsNullOrWhiteSpace(result.EntityName));
		}*/

		[Test]
		[TestCase("Test")]
		[TestCase("Test32")]
		[TestCase("Andrew")]
		[TestCase("Lyle")]
		public async Task Test_Validate_Character_Name_Works_On_Empty_Characters(string name)
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<CharacterController>("Test", 1);
			CharacterController controller = serviceProvider.GetService<CharacterController>();

			//act
			CharacterNameValidationResponse result = ControllerTestsHelpers.GetActionResultObject<CharacterNameValidationResponse>(await controller.ValidateCharacterName(name));

			//assert
			Assert.True(result.isSuccessful);
			Assert.True(result.ResultCode == CharacterNameValidationResponseCode.Success);
		}

		[Test]
		[TestCase("Test")]
		[TestCase("Test32")]
		[TestCase("Andrew")]
		[TestCase("Lyle")]
		public async Task Test_Validate_Character_Name_Fails_On_AlreadyExisting_Character(string name)
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<CharacterController>("Test", 1);
			CharacterController controller = serviceProvider.GetService<CharacterController>();

			//act
			await serviceProvider.GetService<ICharacterRepository>().TryCreateAsync(new CharacterEntryModel(1, name));
			CharacterNameValidationResponse result = ControllerTestsHelpers.GetActionResultObject<CharacterNameValidationResponse>(await controller.ValidateCharacterName(name));

			//assert
			Assert.False(result.isSuccessful, $"Response for name validation should be false when the name is taken.");
			Assert.True(result.ResultCode == CharacterNameValidationResponseCode.NameIsUnavailable);
		}

		[Test]
		[TestCase("Test")]
		[TestCase("Test32")]
		[TestCase("Andrew")]
		[TestCase("Lyle")]
		public async Task Test_Validate_Character_Name_Passes_On_AlreadyExisting_Character_If_The_Names_Are_Different(string name)
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<CharacterController>("Test", 1);
			CharacterController controller = serviceProvider.GetService<CharacterController>();

			//act
			await serviceProvider.GetService<ICharacterRepository>().TryCreateAsync(new CharacterEntryModel(1, $"{name}Z"));
			CharacterNameValidationResponse result = ControllerTestsHelpers.GetActionResultObject<CharacterNameValidationResponse>(await controller.ValidateCharacterName(name));

			//assert
			Assert.True(result.isSuccessful);
			Assert.True(result.ResultCode == CharacterNameValidationResponseCode.Success);
		}

		[Test]
		[TestCase("Test")]
		[TestCase("Test32")]
		[TestCase("Andrew")]
		[TestCase("Lyle")]
		public async Task Test_Can_Create_Character(string name)
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<CharacterController>("Test", 1);
			Mock<IPlayfabCharacterClient> playfabCharacterClientMock = CreatePlayFabCharacterClientMock();
			Mock<ICharacterAppearanceRepository> appearanceRepository = new Mock<ICharacterAppearanceRepository>();
			Mock<ICharacterDataRepository> dataRepository = new Mock<ICharacterDataRepository>();
			CharacterController controller = serviceProvider.GetService<CharacterController>();

			//act
			CharacterCreationResponse result = ControllerTestsHelpers.GetActionResultObject<CharacterCreationResponse>(await controller.CreateCharacter(name, playfabCharacterClientMock.Object, appearanceRepository.Object, dataRepository.Object));

			//assert
			Assert.True(result.isSuccessful);
			Assert.True(result.ResultCode == CharacterCreationResponseCode.Success);
		}

		private static Mock<IPlayfabCharacterClient> CreatePlayFabCharacterClientMock()
		{
			Mock<IPlayfabCharacterClient> playfabCharacterClientMock = new Mock<IPlayfabCharacterClient>();
			playfabCharacterClientMock
				.Setup(p => p.GrantCharacterToUser(It.IsAny<GladMMOPlayFabGrantCharacterToUserRequest>()))
				.Returns(() =>
					Task.FromResult(
						new PlayFabResultModel<GladMMOPlayFabGrantCharacterToUserResult>(
							new GladMMOPlayFabGrantCharacterToUserResult("1234567"))));
			return playfabCharacterClientMock;
		}

		[Test]
		[TestCase("Test")]
		[TestCase("Test32")]
		[TestCase("Andrew")]
		[TestCase("Lyle")]
		public async Task Test_Cannot_Create_Character_With_Duplicate_Name(string name)
		{
			//arrange
			IServiceProvider serviceProvider = BuildServiceProvider<CharacterController>("Test", 1);
			CharacterController controller = serviceProvider.GetService<CharacterController>();
			Mock<IPlayfabCharacterClient> playfabCharacterClientMock = CreatePlayFabCharacterClientMock();
			Mock<ICharacterAppearanceRepository> appearanceRepository = new Mock<ICharacterAppearanceRepository>();
			Mock<ICharacterDataRepository> dataRepository = new Mock<ICharacterDataRepository>();

			//act
			await controller.CreateCharacter(name, playfabCharacterClientMock.Object, appearanceRepository.Object, dataRepository.Object);
			CharacterCreationResponse result = ControllerTestsHelpers.GetActionResultObject<CharacterCreationResponse>(await controller.CreateCharacter(name, playfabCharacterClientMock.Object, appearanceRepository.Object, dataRepository.Object));

			//assert
			Assert.False(result.isSuccessful);
			Assert.AreEqual(CharacterCreationResponseCode.NameUnavailableError, result.ResultCode);
		}

		private static async Task<List<string>> AddTestValuesToRepository(int count, IServiceProvider serviceProvider, int accountId = 1)
		{
			ICharacterRepository repository = serviceProvider.GetService<ICharacterRepository>();

			List<string> names = new List<string>(count);
			//Use the 1 key. Same above we add to the claims
			for(int i = 1; i < count + 1; i++)
			{
				string s = Guid.NewGuid().ToString();
				names.Add(s);

				await repository.TryCreateAsync(new CharacterEntryModel(accountId, s));
			}

			return names;
		}
	}
}
