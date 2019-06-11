using System;
using System.Collections.Generic;
using System.Text;
using VivoxUnity;

namespace GladMMO
{
	/// <summary>
	/// Conctract for publisher of an event that indicates the
	/// voice network has been initialized.
	/// </summary>
	public interface IVoiceNetworkInitializedEventSubscribable
	{
		event EventHandler<VoiceNetworkInitializedEventArgs> OnVoiceNetworkInitialized;
	}

	public sealed class VoiceNetworkInitializedEventArgs : EventArgs
	{
		public VivoxUnity.ILoginSession Session { get; }

		public VoiceNetworkInitializedEventArgs([NotNull] ILoginSession session)
		{
			Session = session ?? throw new ArgumentNullException(nameof(session));
		}
	}
}
