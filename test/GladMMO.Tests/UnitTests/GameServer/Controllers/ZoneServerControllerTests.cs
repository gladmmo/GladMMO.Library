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
	}
}
