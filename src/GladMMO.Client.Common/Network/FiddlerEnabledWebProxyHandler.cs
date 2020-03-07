using System; using FreecraftCore;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace GladMMO
{
	public sealed class FiddlerEnabledWebProxyHandler : HttpClientHandler
	{
		public FiddlerEnabledWebProxyHandler()
		{
			//Proxy = new WebProxy("localhost", 8888);
		}
	}
}
