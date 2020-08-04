using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
#if AZURE_RELEASE || AZURE_DEBUG
				.UseKestrelGuardiansConfigWithStandardEndpoints(args)
#else
				.UseKestrelGuardiansConfig(args)
#endif
				//.UseKestrel()
				.UseIISIntegration()
				.UseStartup<Startup>()
				//TODO: remove this logging when we finally deploy properly
				.UseSetting("detailedErrors", "true")
				.CaptureStartupErrors(true);
	}
}
