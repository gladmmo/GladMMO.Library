using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("spell_effect")]
	public class SpellEffectModel
	{
		/// <summary>
		/// Primary spell effect key.
		/// </summary>
		[Key]
		public int SpellEffectId { get; private set; }

		/// <summary>
		/// Indicates if the spell definition is a default game spell
		/// or if it's a custom user spell.
		/// </summary>
		[Required]
		public bool isDefault { get; private set; }

		/// <summary>
		/// Indicates the type of the effect.
		/// </summary>
		[Required]
		public SpellEffectType EffectType { get; private set; }

		/// <summary>
		/// Modifier combined with entity level and added to <see cref="EffectBasePoints"/> to
		/// compute the effect amount.
		/// </summary>
		[Required]
		public float BasePointsAdditiveLevelModifier { get; private set; }

		/// <summary>
		/// The base effect value before additional scaling.
		/// </summary>
		[Required]
		public int EffectBasePoints { get; private set; }

		/// <summary>
		/// The random component to the effect.
		/// Randomly additive to <see cref="EffectBasePoints"/>.
		/// </summary>
		[Required]
		public int EffectPointsDiceRange { get; private set; }

		/// <summary>
		/// Indicates how an effect should target.
		/// </summary>
		[Required]
		public SpellEffectTargetType EffectTargetingType { get; private set; }

		//Not used yet.
		/// <summary>
		/// Indicates additional information about targeting type.
		/// </summary>
		[Required]
		public SpellEffectTargetType AdditionalEffectTargetingType { get; private set; }

		public SpellEffectModel(bool isDefault, SpellEffectType effectType, float basePointsAdditiveLevelModifier, int effectBasePoints, int effectPointsDiceRange, SpellEffectTargetType effectTargetingType)
		{
			this.isDefault = isDefault;
			EffectType = effectType;
			BasePointsAdditiveLevelModifier = basePointsAdditiveLevelModifier;
			EffectBasePoints = effectBasePoints;
			EffectPointsDiceRange = effectPointsDiceRange;
			EffectTargetingType = effectTargetingType;

			//TODO: Support secondary targeting type.
			AdditionalEffectTargetingType = SpellEffectTargetType.NO_TARGET;
		}

		/// <summary>
		/// EF Ctor.
		/// </summary>
		private SpellEffectModel()
		{
			
		}
	}
}
