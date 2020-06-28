using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	//TODO: Shouldwe use a different enum?? Should we put this somewhere else?
	public enum AuraBuffType
	{
		Positive,
		Negative
	}

	public interface IUIAuraBuffCollection
	{
		IUIAuraBuffSlot this[AuraBuffType type, byte index] { get; }
	}
}
