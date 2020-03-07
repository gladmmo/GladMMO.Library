using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//Based on WoW.
	//See: https://raw.githubusercontent.com/FreecraftCore/FreecraftCore.DBCTools/7600c69732d9c31f0762304cd3ab8757a2859314/src/FreecraftCore.DBC.Common/Spell/SpellDamageClassType.cs
	public enum SpellClassType
	{
		Normal = 0,
		Magic = 1,
		Melee = 2,
		Ranged = 3
	};
}
