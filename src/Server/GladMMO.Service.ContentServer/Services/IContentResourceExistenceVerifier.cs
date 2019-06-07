using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IContentResourceExistenceVerifier
	{
		Task<bool> VerifyResourceExists(UserContentType contentType, Guid contentGuid);
	}
}
