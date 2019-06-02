using System;
using System.Collections.Generic;
using System.Text;
using GladNet;

namespace GladMMO
{
	public interface ISessionDisconnectionEventSubscribable
	{
		event EventHandler<SessionStatusChangeEventArgs> OnSessionDisconnection;
	}
}
