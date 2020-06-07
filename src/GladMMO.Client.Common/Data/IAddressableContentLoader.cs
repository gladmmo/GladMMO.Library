using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IAddressableContentLoader
	{
		Task<T> LoadContentAsync<T>(string address);
	}
}
