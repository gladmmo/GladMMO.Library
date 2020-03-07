using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class BypassHttpsValidationHandler : HttpClientHandler
	{
		public BypassHttpsValidationHandler()
		{
			this.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true;
		}
	}
}
