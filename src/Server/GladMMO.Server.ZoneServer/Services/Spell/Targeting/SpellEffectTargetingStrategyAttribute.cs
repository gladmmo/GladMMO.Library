using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class SpellEffectTargetingStrategyAttribute : Attribute
	{
		public SpellEffectTargetType EffectTargetingType { get; private set; }

		public SpellEffectTargetType AdditionalEffectTargetingType { get; private set; }

		public SpellEffectTargetingStrategyAttribute(SpellEffectTargetType effectTargetingType, SpellEffectTargetType additionalEffectTargetingType)
		{
			if (!Enum.IsDefined(typeof(SpellEffectTargetType), effectTargetingType)) throw new InvalidEnumArgumentException(nameof(effectTargetingType), (int) effectTargetingType, typeof(SpellEffectTargetType));
			if (!Enum.IsDefined(typeof(SpellEffectTargetType), additionalEffectTargetingType)) throw new InvalidEnumArgumentException(nameof(additionalEffectTargetingType), (int) additionalEffectTargetingType, typeof(SpellEffectTargetType));

			EffectTargetingType = effectTargetingType;
			AdditionalEffectTargetingType = additionalEffectTargetingType;
		}
	}
}
