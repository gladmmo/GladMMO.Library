using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for avatar entry repository types.
	/// </summary>
	public interface IAvatarEntryRepository : IGenericRepositoryCrudable<long, AvatarEntryModel>
	{
		Task SetWorldValidated(long worldId);
	}
}
