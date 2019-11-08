using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ISpellEffectHandler
	{
		void ApplySpellEffect(SpellEffectApplicationContext context);
	}
}
