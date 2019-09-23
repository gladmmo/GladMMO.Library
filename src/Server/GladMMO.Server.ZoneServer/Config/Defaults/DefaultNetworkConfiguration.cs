using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultNetworkConfiguration : NetworkConfiguration
	{
		public DefaultNetworkConfiguration()
			: base(5006, "192.168.0.12")
		{
			
		}
	}
}
