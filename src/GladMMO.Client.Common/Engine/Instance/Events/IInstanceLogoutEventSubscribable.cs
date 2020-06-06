using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IInstanceLogoutEventSubscribable
	{
		event EventHandler OnInstanceLogout;
	}
}
