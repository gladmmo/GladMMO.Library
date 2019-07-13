using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Data model for a connection endpoint.
	/// </summary>
	public interface IGameConnectionEndpointDetails
	{
		/// <summary>
		/// The port for the connection endpoint.
		/// </summary>
		int Port { get; set; }

		/// <summary>
		/// The IpAddress for the connection endpoint.
		/// </summary>
		string IpAddress { get; set; }
	}
}
