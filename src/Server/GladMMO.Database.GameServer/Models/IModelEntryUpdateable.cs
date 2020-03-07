using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that a data models that can be updated
	/// with <typeparamref name="TModelType"/>.
	/// </summary>
	/// <typeparam name="TModelType">The model type used to update the model.</typeparam>
	public interface IModelEntryUpdateable<in TModelType>
	{
		/// <summary>
		/// Updates the model with data from <see cref="model"/>.
		/// </summary>
		/// <param name="model">The model to be used to update from.</param>
		void Update(TModelType model);
	}
}
