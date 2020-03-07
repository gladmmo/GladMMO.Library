using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IEntireTableQueryable<TContentType>
	{
		Task<TContentType[]> RetrieveAllAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
