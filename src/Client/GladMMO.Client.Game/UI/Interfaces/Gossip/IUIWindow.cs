using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIWindow
	{
		IUIElement RootElement { get; }
	}
}
