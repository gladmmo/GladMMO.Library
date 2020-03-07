using System; using FreecraftCore;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GladMMO
{
	public class X509Certificate2Loader
	{
		/// <summary>
		/// Creates a new X509Certificate2 loader.
		/// </summary>
		/// <param name="certPath">The path to the cert that should be loaded.</param>
		/// <returns>An instance of the loader.</returns>
		public static X509Certificate2Loader Create(string certPath)
		{
			return new X509Certificate2Loader(certPath);
		}

		private string CertPath { get; }

		public X509Certificate2Loader(string certPath)
		{
			if (string.IsNullOrWhiteSpace(certPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(certPath));

			CertPath = certPath;
		}

		/// <summary>
		/// Loads the <see cref="X509Certificate2"/>
		/// </summary>
		/// <returns>The certificate loaded.</returns>
		public X509Certificate2 Load()
		{
			X509Certificate2 cert = null;

			//We only try the file store for now
			new FileStoreX509Certificate2LoadingStrategy().TryLoadCertificate(CertPath, out cert);

			if(cert == null)
				throw new InvalidOperationException($"Failed to load the certifcate {CertPath}.");

			return cert;
		}
	}
}
