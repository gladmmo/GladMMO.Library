using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ISpellEffectDefinition
	{
		/// <summary>
		/// Primary spell effect key.
		/// </summary>
		int SpellEffectId { get;}

		/// <summary>
		/// Indicates the type of the effect.
		/// </summary>
		SpellEffectType EffectType { get; }

		/// <summary>
		/// Modifier combined with entity level and added to <see cref="EffectBasePoints"/> to
		/// compute the effect amount.
		/// </summary>
		float BasePointsAdditiveLevelModifier { get; }

		/// <summary>
		/// The base effect value before additional scaling.
		/// </summary>
		int EffectBasePoints { get;  }

		/// <summary>
		/// The random component to the effect.
		/// Randomly additive to <see cref="EffectBasePoints"/>.
		/// </summary>
		int EffectPointsDiceRange { get; }

		/// <summary>
		/// Indicates how an effect should target.
		/// </summary>
		SpellEffectTargetType EffectTargetingType { get; }

		//Not used yet.
		/// <summary>
		/// Indicates additional information about targeting type.
		/// </summary>
		SpellEffectTargetType AdditionalEffectTargetingType { get; }
	}
}
