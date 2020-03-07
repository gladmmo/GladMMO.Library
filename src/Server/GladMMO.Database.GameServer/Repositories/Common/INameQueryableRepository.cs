using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface INameQueryableRepository<in TKeyType>
	{
		/// <summary>
		/// Retrieves the name of the model by the provided
		/// <see cref="key"/>.
		/// </summary>
		/// <param name="key">The key to lookup.</param>
		/// <returns>The name of the model. Throws if it doesn't exist.</returns>
		Task<string> RetrieveNameAsync(TKeyType key);
	}
}
