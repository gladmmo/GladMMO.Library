using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityDataChangeCallbackService
	{
		/// <summary>
		/// Invokes any registered change callbacks for the <see cref="entity"/>
		/// if callbacks have been registered for changes involving <see cref="field"/>.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="fieldContainer"></param>
		/// <param name="field"></param>
		void InvokeChangeEvents(ObjectGuid entity, IEntityDataFieldContainer fieldContainer, int field);
	}
}
