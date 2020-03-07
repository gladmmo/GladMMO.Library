using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ILoginButtonClickedEventSubscribable
	{
		event EventHandler OnLoginButtonClicked;
	}
}
