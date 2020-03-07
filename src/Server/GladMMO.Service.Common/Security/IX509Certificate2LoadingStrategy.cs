using System; using FreecraftCore;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Strategy for loading a certificate.
	/// </summary>
	public interface IX509Certificate2LoadingStrategy
	{
		/// <summary>
		/// Tries to load a <see cref="X509Certificate2"/>.
		/// </summary>
		/// <param name="name">The name of the certificate.</param>
		/// <param name="cert">The certificate out parameter. Should be null.</param>
		/// <returns>Indicates if the certificate has been loaded.</returns>
		bool TryLoadCertificate(string name, out X509Certificate2 cert);
	}
}
