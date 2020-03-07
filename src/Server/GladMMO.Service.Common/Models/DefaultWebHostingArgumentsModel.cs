using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace GladMMO
{
	/// <summary>
	/// Default command line hosting options for HaloLive web services.
	/// </summary>
	public sealed class DefaultWebHostingArgumentsModel
	{
		/// <summary>
		/// Command line option --url={url}.
		/// Specifies the URL {url} to host the web service on.
		/// </summary>
		[Option('u', "url")]
		public string Url { get; private set; } //For CommandLineParser 2.1.1-beta we must have private setters

		/// <summary>
		/// Indicates if a custom URL is defined.
		/// </summary>
		public bool isCustomUrlDefined => !String.IsNullOrEmpty(Url);

		/// <summary>
		/// Command line options --usehttps={CertName}.
		/// Indicates if HTTPS should be enabled and what cert to use.
		/// </summary>
		[Option('h', "usehttps")]
		public string HttpsCertificateName { get; private set; } //For CommandLineParser 2.1.1-beta we must have private setters

		/// <summary>
		/// Indicates if https has been enabled with a custom cert defined.
		/// </summary>
		public bool isHttpsEnabled => !String.IsNullOrEmpty(HttpsCertificateName);
	}
}
