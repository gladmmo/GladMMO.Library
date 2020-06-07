using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ILocalPlayerDetails : IReadonlyLocalPlayerDetails
	{
		new ObjectGuid LocalPlayerGuid { get; set; }
	}

	public interface IReadonlyLocalPlayerDetails
	{
		ObjectGuid LocalPlayerGuid { get; }

		IEntityDataFieldContainer EntityData { get; }
	}
}
