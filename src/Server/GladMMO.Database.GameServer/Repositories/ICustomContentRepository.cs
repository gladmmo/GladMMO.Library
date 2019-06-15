using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for repository that provide generic methods for <see cref="IClientContentPersistable"/> model types.
	/// </summary>
	/// <typeparam name="TCustomModelType">The <see cref="IClientContentPersistable"/> type.</typeparam>
	public interface ICustomContentRepository<TCustomModelType> : IGenericRepositoryCrudable<long, TCustomModelType>
		where TCustomModelType : IClientContentPersistable
	{

	}
}
