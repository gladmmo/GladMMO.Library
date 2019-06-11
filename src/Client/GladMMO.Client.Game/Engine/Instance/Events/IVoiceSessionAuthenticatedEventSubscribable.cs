using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IVoiceSessionAuthenticatedEventSubscribable
	{
		event EventHandler<VoiceSessionAuthenticatedEventArgs> OnVoiceSessionAuthenticated;
	}

	public sealed class VoiceSessionAuthenticatedEventArgs : EventArgs
	{
		public VivoxUnity.ILoginSession Session { get; }

		public VoiceSessionAuthenticatedEventArgs([NotNull] VivoxUnity.ILoginSession session)
		{
			Session = session ?? throw new ArgumentNullException(nameof(session));
		}
	}
}
