using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for data types that can be updated from a remote model.
	/// </summary>
	/// <typeparam name="TRemoteModelType">The remote model type.</typeparam>
	public interface IRemoteModelUpdateable<in TRemoteModelType>
	{
		/// <summary>
		/// Updates the model with the remote <see cref="model"/> counterpart's data.
		/// </summary>
		/// <param name="model"></param>
		void Update(TRemoteModelType model);
	}
}
