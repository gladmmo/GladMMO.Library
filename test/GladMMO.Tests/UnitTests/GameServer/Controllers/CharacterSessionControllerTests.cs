using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public class CharacterSessionControllerTests
	{
		[Test]
		public static async Task Test_Controller_Produces_InvalidId_When_Empty()
		{
			//arrange
			IServiceProvider serviceProvider = ControllerTestsHelpers.BuildServiceProvider<CharacterSessionController>("Test", 1);
			CharacterSessionController controller = serviceProvider.GetService<CharacterSessionController>();

			//act
			CharacterSessionEnterResponse response = await controller.EnterSession(5);

			//assert
			Assert.False(response.isSuccessful);
			Assert.AreEqual(CharacterSessionEnterResponseCode.InvalidCharacterIdError, response.ResultCode);
		}

		//This is EXTREMELY important. We DO NOT want to allow anyone with an active session
		[Test]
		public static async Task Test_Controller_Produces_AlreadyHasActiveSession_When_Session_Has()
		{
			//arrange
			IServiceProvider serviceProvider = ControllerTestsHelpers.BuildServiceProvider<CharacterSessionController>("Test", 1);
			CharacterSessionController controller = serviceProvider.GetService<CharacterSessionController>();
			ICharacterRepository characterRepo = serviceProvider.GetService<ICharacterRepository>();
			ICharacterSessionRepository sessionRepo = serviceProvider.GetService<ICharacterSessionRepository>();

			await characterRepo.TryCreateAsync(new CharacterEntryModel(1, "Testing"));
			await sessionRepo.TryCreateAsync(new CharacterSessionModel(1, 0));

			//We can't create the claimed session through this interface because it's a stored procedure.
			//Raw SQL can't execute. So we must interact directly with the DbSet
			//await sessionRepo.TryClaimUnclaimedSession(1, 1);
			CharacterDatabaseContext context = serviceProvider.GetService<CharacterDatabaseContext>();
			await context.ClaimedSession.AddAsync(new ClaimedSessionsModel(1));
			await context.SaveChangesAsync();

			//act
			CharacterSessionEnterResponse response = await controller.EnterSession(1);

			//assert
			Assert.False(response.isSuccessful, $"Characters that already have ");
			Assert.AreEqual(CharacterSessionEnterResponseCode.AccountAlreadyHasCharacterSession, response.ResultCode);
		}

		[Test]
		public static async Task Test_Controller_Produces_InvalidId_When_Wrong_AccountId()
		{
			//arrange
			IServiceProvider serviceProvider = ControllerTestsHelpers.BuildServiceProvider<CharacterSessionController>("Test", 2);
			CharacterSessionController controller = serviceProvider.GetService<CharacterSessionController>();
			ICharacterRepository characterRepo = serviceProvider.GetService<ICharacterRepository>();
			ICharacterSessionRepository sessionRepo = serviceProvider.GetService<ICharacterSessionRepository>();

			await characterRepo.TryCreateAsync(new CharacterEntryModel(1, "Testing"));
			await sessionRepo.TryCreateAsync(new CharacterSessionModel(1, 0));

			//act
			CharacterSessionEnterResponse response = await controller.EnterSession(1);

			//assert
			Assert.False(response.isSuccessful, $"Characters should not be able to create sessions when the accountid doesn't match.");
			Assert.AreEqual(response.ResultCode, CharacterSessionEnterResponseCode.InvalidCharacterIdError);
		}

		//This is EXTREMELY important. We DO NOT want to allow anyone with an active session
		[Test]
		[TestCase(5, 6)]
		[TestCase(5, 7)]
		[TestCase(5, 7)]
		[TestCase(3, short.MaxValue)]
		public static async Task Test_Controller_Produces_SessionGranted_With_Zone_Id_If_UnclaimedSession_Exists(int accountId, int zoneid)
		{
			//arrange
			IServiceProvider serviceProvider = ControllerTestsHelpers.BuildServiceProvider< CharacterSessionController>("Test", accountId);
			CharacterSessionController controller = serviceProvider.GetService<CharacterSessionController>();
			ICharacterRepository characterRepo = serviceProvider.GetService<ICharacterRepository>();
			ICharacterSessionRepository sessionRepo = serviceProvider.GetService<ICharacterSessionRepository>();

			await characterRepo.TryCreateAsync(new CharacterEntryModel(accountId, "Testing"));
			await sessionRepo.TryCreateAsync(new CharacterSessionModel(1, zoneid));

			//act
			CharacterSessionEnterResponse response = await controller.EnterSession(1);

			//assert
			Assert.True(response.isSuccessful, $"Created sessions should be granted if no active account session or character session is claimed.");
			Assert.AreEqual(CharacterSessionEnterResponseCode.Success, response.ResultCode);
			Assert.AreEqual(zoneid, response.ZoneId, $"Provided zone id was not the same as the session.");
		}

		[TestCase(7)]
		[TestCase(1)]
		[TestCase(25)]
		[TestCase(short.MaxValue)]
		public static async Task Test_Controller_Creates_UnclaimedSession_On_OnEnterSession(int accountId)
		{
			//arrange
			IServiceProvider serviceProvider = ControllerTestsHelpers.BuildServiceProvider<CharacterSessionController>("Test", accountId);
			CharacterSessionController controller = serviceProvider.GetService<CharacterSessionController>();
			ICharacterRepository characterRepo = serviceProvider.GetService<ICharacterRepository>();

			await characterRepo.TryCreateAsync(new CharacterEntryModel(accountId, "Testing"));

			//act: We also test that we can do it multiple times
			CharacterSessionEnterResponse response = await controller.EnterSession(1);

			//assert
			Assert.True(response.isSuccessful);
			Assert.AreEqual(CharacterSessionEnterResponseCode.Success, response.ResultCode);
		}
	}
}
