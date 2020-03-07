using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using static GladMMO.ControllerTestsHelpers;

namespace GladMMO
{
	[TestFixture]
	public class ZoneServerControllerTests
	{
		[Test]
		public async Task Test_Can_Create_Controller()
		{
			//arrange
			IServiceProvider provider = BuildServiceProvider<ZoneServerController>("Test", 1);

			//assert
			Assert.DoesNotThrow(() => provider.GetService<ZoneServerController>());
		}

		[Test]
		public async Task Test_ZoneServer_GetEndpoint_ReturnsFail_On_Empty()
		{
			//arrange
			IServiceProvider provider = BuildServiceProvider<ZoneServerController>("Test", 1);
			ZoneServerController controller = provider.GetService<ZoneServerController>();

			//assert
			ResolveServiceEndpointResponse result = GetActionResultObject<ResolveServiceEndpointResponse>(await controller.GetServerEndpoint(1));

			//assert
			Assert.False(result.isSuccessful);
		}

		[Test]
		[TestCase(25)]
		[TestCase(short.MaxValue)]
		[TestCase(int.MaxValue)]
		public async Task Test_ZoneServer_GetEndpoint_ReturnsFail_On_NoExistingZoneId(int zoneId)
		{
			//arrange
			IServiceProvider provider = BuildServiceProvider<ZoneServerController>("Test", 1);
			ZoneServerController controller = provider.GetService<ZoneServerController>();
			IZoneServerRepository repo = provider.GetService<IZoneServerRepository>();
			await repo.TryCreateAsync(new ZoneInstanceEntryModel(1, "127.0.0.1", 1080, 1));
			await repo.TryCreateAsync(new ZoneInstanceEntryModel(2, "127.0.0.1", 1080, 1));
			await repo.TryCreateAsync(new ZoneInstanceEntryModel(3, "127.0.0.1", 1080, 1));

			//assert
			ResolveServiceEndpointResponse result = GetActionResultObject<ResolveServiceEndpointResponse>(await controller.GetServerEndpoint(25));

			//assert
			Assert.False(result.isSuccessful);
		}

		[Test]
		[TestCase("127.0.0.1", 5050)]
		[TestCase("www.example.com", 80)]
		public async Task Test_ZoneServer_GetEndpoint_Succeeds_On_Known_Id(string endpoint, int port)
		{
			//arrange
			IServiceProvider provider = BuildServiceProvider<ZoneServerController>("Test", 1);
			ZoneServerController controller = provider.GetService<ZoneServerController>();
			IZoneServerRepository repo = provider.GetService<IZoneServerRepository>();
			await repo.TryCreateAsync(new ZoneInstanceEntryModel(1, endpoint, (short)port, 1));

			//assert
			ResolveServiceEndpointResponse result = GetActionResultObject<ResolveServiceEndpointResponse>(await controller.GetServerEndpoint(1));

			//assert
			Assert.True(result.isSuccessful);
			Assert.AreEqual(ResolveServiceEndpointResponseCode.Success, result.ResultCode);
			Assert.AreEqual(endpoint, result.Endpoint.EndpointAddress);
			Assert.AreEqual(port, result.Endpoint.EndpointPort);
		}
	}
}
