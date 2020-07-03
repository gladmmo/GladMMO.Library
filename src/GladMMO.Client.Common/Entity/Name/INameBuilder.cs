using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface INameBuilder
	{
		string Build();

		bool IsBuildable();
	}
}
