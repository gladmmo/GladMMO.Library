using System; using FreecraftCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
				//Even if not on Azure we need the default endpoints.
				.UseKestrelGuardiansConfigWithStandardEndpoints(args)
				.UseIISIntegration()
				//Only way: https://stackoverflow.com/a/51450471
				.ConfigureServices(collection =>
					{
						//Added for things can address the current endpoint.
#if AZURE_RELEASE || AZURE_DEBUG
						//TODO: Don't hardcode the endpoint here.
						collection.AddSingleton(new PreferredEndpoint("http://test-guardians-auth.azurewebsites.net", 80));
#else
						collection.AddSingleton(new PreferredEndpoint("https://127.0.0.1", 443));
#endif
					})
				.UseStartup<Startup>()
				.ConfigureAppConfiguration((context, builder) =>
				{
					//We now reigter this out here in ASP Core 2.0
					builder.AddJsonFile(@"Config/authserverconfig.json", false);
				})
				//TODO: remove this logging when we finally deploy properly
				.UseSetting("detailedErrors", "true")
				.CaptureStartupErrors(true)
				.Build();
	}
}
