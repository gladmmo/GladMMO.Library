using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Strategy for loading the cert from the file store.
	/// It expects the cert to be loaded in the certs folder.
	/// </summary>
	public class FileStoreX509Certificate2LoadingStrategy : IX509Certificate2LoadingStrategy
	{
		/// <inheritdoc />
		public bool TryLoadCertificate(string name, out X509Certificate2 cert)
		{
#if NETCOREAPP1_1
			throw new NotSupportedException($"Cert loader isn't working on netcore1.1 right now.");
#else
			if (File.Exists(name))
			{
				try
				{
					//The below will not work for local. Though behind IIS in AWS we must do the below
#if !DEBUG_LOCAL && !RELEASE_LOCAL
					cert = new X509Certificate2(name, "", X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.MachineKeySet);
#else
					cert = new X509Certificate2(name);
#endif
				}
				catch (Exception e)
				{
					//Maybe it has a password
					//The below will not work for local. Though behind IIS in AWS we must do the below
#if !DEBUG_LOCAL && !RELEASE_LOCAL
					cert = new X509Certificate2(name, "Test", X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.MachineKeySet);
#else
					cert = new X509Certificate2(name, "Test");
#endif
				}

				return true;
			}

			cert = null;
			return false;
#endif
			}
	}
}
