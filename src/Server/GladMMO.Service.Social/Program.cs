using System; using FreecraftCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace GladMMO
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
#if AZURE_RELEASE || AZURE_DEBUG
				.UseKestrelGuardiansConfigWithStandardEndpoints(args)
#else
				.UseKestrelGuardiansConfig(args)
#endif
				.ConfigureServices(services => services.AddAutofac()) //this enables AutoFac configuration support
				//.UseKestrel()
				.UseIISIntegration()
				.UseStartup<Startup>()
				//TODO: remove this logging when we finally deploy properly
				.UseSetting("detailedErrors", "true")
				.CaptureStartupErrors(true)
				.Build();
	}
}