using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Conctract for publisher of an event that indicates the
	/// voice network has been initialized.
	/// </summary>
	public interface IVoiceNetworkInitializedEventSubscribable
	{
		event EventHandler OnVoiceNetworkInitialized;
	}
}
