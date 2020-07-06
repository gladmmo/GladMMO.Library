using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ITrinityGuildRepository : IGenericRepositoryCrudable<uint, Guild>, INameQueryableRepository<uint>
	{

	}
}
