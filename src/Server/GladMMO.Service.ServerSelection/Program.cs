using System; using FreecraftCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
				.UseKestrelGuardiansConfig(args)
				.UseIISIntegration()
				.UseStartup<Startup>()
				//TODO: remove this logging when we finally deploy properly
				.UseSetting("detailedErrors", "true")
				.CaptureStartupErrors(true)
				.Build();
	}
}