using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace GladMMO
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class SpellCastValidatorAttribute : Attribute
	{
		public SpellCastValidatorAttribute()
		{
			
		}
	}
}
