using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Amazon.CloudWatchLogs.Model;
using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace GladMMO
{
	public static class IWebBuilderExtensions
	{
		public static IWebHostBuilder UseKestrelGuardiansConfigWithStandardEndpoints(this IWebHostBuilder builder, string[] args)
		{
			builder.UseSetting("https_port", "443")
				.UseSetting("http_endpoint", "0.0.0.0")
				.UseSetting("http_port", "80")
				.UseSetting("https_endpoint", @"0.0.0.0")
				.UseUrls(@"http://0.0.0.0:80", @"https://0.0.0.0:443");

			builder.ConfigureKestrelHostWithCommandlinArgs(args, false);

			return builder;
		}

		/// <summary>
		/// Registers Kestrl using <see cref="WebHostBuilderKestrelExtensions"/>'s UseKestrl.
		/// Optionally abstracts configuration for local vs deploy vs AWS registeration.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The builder.</returns>
		public static IWebHostBuilder UseKestrelGuardiansConfig(this IWebHostBuilder builder, string[] args, bool shouldUseDefaultUrlIfNoneProvided = true)
		{
			//If we're local then we don't want to set this IP
			//because it won't work. Since we won't be on our own EC2 instance with IIS
#if !DEBUG_LOCAL && !RELEASE_LOCAL
			builder
			.UseKestrel()
			.UseUrls(@"http://0.0.0.0:7777");
#else
			builder.ConfigureKestrelHostWithCommandlinArgs(args, shouldUseDefaultUrlIfNoneProvided);
#endif

			return builder;
		}

		/// <summary>
		/// Configure the server host with the arguments encoded in the <see cref="args"/> mapped to
		/// an options instance of <see cref="DefaultWebHostingArgumentsModel"/>.
		/// </summary>
		/// <param name="builder">The web host builder.</param>
		/// <param name="args">The commandline args.</param>
		/// <returns>The provided <see cref="IWebHostBuilder"/>for fluent chaining.</returns>
		public static IWebHostBuilder ConfigureKestrelHostWithCommandlinArgs(this IWebHostBuilder builder, string[] args, bool shouldUseDefaultUrlIfNoneProvided = true)
		{
			Parser.Default.ParseArguments<DefaultWebHostingArgumentsModel>(args)
				.WithParsed(model =>
				{
					List<string> endpoints = new List<string>();

					//If https is enabled then a certifcate should be available for loading.
					builder.UseKestrel(options =>
					{
						//Get the port
						if (model.isCustomUrlDefined)
						{
							
							foreach (var entry in model.Url)
							{
								int port = 5000;
								int.TryParse(entry.Split(':').Last(), out port);

								//Idea here is that if they specified the port, then we can actually use it as the HTTPS port setting.
								if (entry.Contains("https"))
									builder.UseSetting("https_port", port.ToString());
								else
									builder.UseSetting("http_port", port.ToString());

								//Remov http
								string ip = entry.Replace("http://", "");
								ip = ip.Replace("https://", "");
								if (ip.Contains(':'))
									ip = ip.Split(':').First();

								var modelUrl = entry;

								//TODO: This won't actually work, it's not an IP.
								if (entry.Contains("https"))
									builder.UseSetting("https_endpoint", entry.Replace($":{port}", "").Replace(@"https://", "").Replace("www.", ""));
								else
									builder.UseSetting("http_endpoint", entry.Replace($":{port}", "").Replace(@"http://", "").Replace("www.", ""));

								endpoints.Add(modelUrl);
							}
						}
						else if(shouldUseDefaultUrlIfNoneProvided)
						{
							string prefix = model.isHttpsEnabled ? @"https://" : @"http://";
							builder.UseUrls($@"{prefix}localhost:5000");
						}

						//Check if we have an http setting set
						string potentialHttpValue = builder.GetSetting("http_endpoint");
						if(!String.IsNullOrWhiteSpace(potentialHttpValue))
							options.Listen(new IPEndPoint(IPAddress.Parse(builder.GetSetting("http_endpoint")), Int32.Parse(builder.GetSetting("http_port"))));

						//.UseSetting("https_port", "443")
						if (model.isHttpsEnabled)
						{
							try
							{
								options.Listen(new IPEndPoint(IPAddress.Parse(builder.GetSetting("https_endpoint")), Int32.Parse(builder.GetSetting("https_port"))), listenOptions =>
								{
									if(model.isHttpsEnabled)
									{
										var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions()
										{
											//TODO: Do we need this in ASP Core 2.0?
											//ClientCertificateMode = ClientCertificateMode.AllowCertificate,

											//TODO: Mono doesn't support Tls1 or Tls2 and we have no way to config this. 
											//Ssl3 is mostly safe and supported by Mono which means it will work in Unity3D now.
											SslProtocols = System.Security.Authentication.SslProtocols.Tls
											               | System.Security.Authentication.SslProtocols.Tls11
											               | System.Security.Authentication.SslProtocols.Tls12,

											ServerCertificate = X509Certificate2Loader.Create(model.HttpsCertificateName).Load()
										};

										listenOptions.UseHttps(httpsConnectionAdapterOptions);
									}
								});
							}
							catch (Exception e)
							{
								throw new System.InvalidOperationException($"Failed to register HTTPS Endpoint: {builder.GetSetting("https_endpoint")}:{builder.GetSetting("https_port")} Reason: {e.Message}", e);
							}
						}
					})
						.UseUrls(endpoints.ToArray());
				});

			return builder;
		}
	}
}
