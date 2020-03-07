using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Contract for collections that can have entries
	/// removed via a <see cref="ObjectGuid"/>.
	/// </summary>
	public interface IEntityCollectionRemovable : Glader.Essentials.IEntityCollectionRemovable<ObjectGuid>
	{

	}
}
