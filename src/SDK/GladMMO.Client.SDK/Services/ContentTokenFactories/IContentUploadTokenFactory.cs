using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IContentUploadTokenFactory : IFactoryCreatable<Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>>, ContentUploadTokenFactoryContext>
	{

	}
}
