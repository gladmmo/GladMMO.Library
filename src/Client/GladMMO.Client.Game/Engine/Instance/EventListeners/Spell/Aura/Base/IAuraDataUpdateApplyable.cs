using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IAuraDataUpdateApplyable
	{
		void ApplyAuraData(IAuraApplicationDataEventContainer data);
	}
}
