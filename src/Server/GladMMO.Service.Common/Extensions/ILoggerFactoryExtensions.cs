using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public static class ILoggerFactoryExtensions
	{
		public static ILoggerFactory RegisterGuardiansLogging(this ILoggerFactory loggerFactory, IConfiguration configuration)
		{
			//We don't want to use AWS cloud watch on a local running.
			//We just want the console.
/*#if !DEBUG_LOCAL && !RELEASE_LOCAL
			//This adds CloudWatch AWS logging to this app
			loggerFactory.AddAWSProvider(configuration.GetAWSLoggingConfigSection());
#else
			loggerFactory.AddConsole(configuration.GetSection("Logging"));
#endif*/
			//return loggerFactory.AddConsole(configuration.GetSection("Logging"));

			return loggerFactory;
		}
	}
}
