using System;
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
				//HelloKitty: This is actually a very special case, the authentication server has 3rd party dependencies now from PlayFab. It will attempt to connect in a very specific way.
				.UseSetting("https_port", "443")
				.UseSetting("http_endpoint", "0.0.0.0")
				.UseSetting("http_port", "80")
				.UseSetting("https_endpoint", @"0.0.0.0")
				.UseUrls(@"http://0.0.0.0:80", @"https://0.0.0.0:443")
				.UseKestrelGuardiansConfig(args, false)
				.UseIISIntegration()
				//Only way: https://stackoverflow.com/a/51450471
				.ConfigureServices(collection =>
					{
						//Added for things can address the current endpoint.
						collection.AddSingleton(new PreferredEndpoint("https://127.0.0.1", 443));
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
				.UseApplicationInsights()
				.Build();
	}
}
