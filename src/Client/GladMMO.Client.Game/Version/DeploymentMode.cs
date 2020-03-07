using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of client deployment modes.
	/// </summary>
	public enum DeploymentMode
	{
		/// <summary>
		/// Indicates the client is deployed locally and will connect to local services.
		/// </summary>
		Local = 1,

		/// <summary>
		/// Indicates the client is deployed to connect to Azure's GladMMO test servers.
		/// </summary>
		AzureTest = 2,

		/// <summary>
		/// Indicates the client is deployed to connect to Azure's GladMMO production servers.
		/// </summary>
		AzureProduction = 3,
	}
}
