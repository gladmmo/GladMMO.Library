using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for aura data updatable.
	/// </summary>
	public interface IAuraDataUpdateApplyable
	{
		/// <summary>
		/// Calls will apply or re-apply aura data.
		/// </summary>
		/// <param name="data">The data.</param>
		void ApplyAuraData(IAuraApplicationDataEventContainer data);
	}
}
