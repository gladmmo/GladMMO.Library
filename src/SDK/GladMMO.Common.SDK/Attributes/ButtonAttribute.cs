using System; using FreecraftCore;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GladMMO
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class ButtonAttribute : Attribute
	{

	}
}
