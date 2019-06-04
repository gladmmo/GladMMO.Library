using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace GladMMO
{
	public static class ControllerTestsHelpers
	{
		public static T GetActionResultObject<T>(IActionResult result)
		{
			if(result == null) throw new ArgumentNullException(nameof(result));

			ObjectResult objectResult = (result as ObjectResult);

			if(objectResult?.Value == null) throw new InvalidOperationException($"Failed to get object Type: {typeof(T).Name} from IActionResult.");

			if(!typeof(T).IsAssignableFrom(objectResult.Value.GetType()))
				throw new InvalidOperationException($"Result objects is not of Type: {typeof(T).Name} was Type: {objectResult.Value.GetType().Name}");

			return (T)objectResult.Value;
		}

		public static TControllerType BuildCharacterSessionController<TControllerType>(string userName, int accountId)
			where TControllerType : Controller
		{
			return BuildServiceProvider<TControllerType>(userName, accountId).GetService<TControllerType>();
		}

		public static IServiceProvider BuildServiceProvider<TControllerType>(string userName, int accountId)
			where TControllerType : Controller
		{
			Mock<IClaimsPrincipalReader> claimsReaderMock = new Mock<IClaimsPrincipalReader>();

			claimsReaderMock.Setup(c => c.GetUserName(It.IsAny<ClaimsPrincipal>()))
				.Returns(() => userName);

			claimsReaderMock.Setup(c => c.GetUserId(It.IsAny<ClaimsPrincipal>()))
				.Returns(accountId.ToString);

			Mock<ILogger<TControllerType>> loggingMock = new Mock<ILogger<TControllerType>>();

			IServiceProvider serviceProvider = new ServiceCollection()
				.AddTestDatabaseContext<CharacterDatabaseContext>()
				.AddDefaultDataTestServices()
				.AddSingleton<ICharacterRepository, DatabaseBackedCharacterRepository>()
				.AddSingleton<ICharacterSessionRepository, DatabaseBackedCharacterSessionRepository>()
				.AddSingleton<IZoneServerRepository, DatabaseBackedZoneServerRepository>()
				.AddTransient<TControllerType>()
				.AddTransient<IClaimsPrincipalReader>(provider => claimsReaderMock.Object)
				.AddTransient<ILogger<TControllerType>>(provider => loggingMock.Object)
				.BuildServiceProvider();

			return serviceProvider;
		}

		public static TControllerType BuildCharacterController<TControllerType>() 
			where TControllerType : Controller
		{
			return BuildCharacterSessionController<TControllerType>("Test", 1);
		}
	}
}
