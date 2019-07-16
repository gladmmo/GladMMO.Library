using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Contract for collections that can have entries
	/// removed via a <see cref="NetworkEntityGuid"/>.
	/// </summary>
	public interface IEntityCollectionRemovable : Glader.Essentials.IEntityCollectionRemovable<NetworkEntityGuid>
	{

	}
}
