using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class SpellEffectHandlerAttribute : Attribute
	{
		public SpellEffectType EffectType { get; }

		public SpellEffectHandlerAttribute(SpellEffectType effectType)
		{
			if (!Enum.IsDefined(typeof(SpellEffectType), effectType)) throw new InvalidEnumArgumentException(nameof(effectType), (int) effectType, typeof(SpellEffectType));

			EffectType = effectType;
		}
	}
}
