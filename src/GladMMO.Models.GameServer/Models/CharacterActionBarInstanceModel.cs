using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterActionBarInstanceModel
	{
		/// <summary>
		/// The index for action bar this model instance is for.
		/// </summary>
		[JsonProperty]
		public ActionBarIndex BarIndex { get; private set; }

		/// <summary>
		/// The type of action this bar index is linked to.
		/// </summary>
		[JsonProperty]
		public FreecraftCore.ActionButtonType Type { get; private set; }

		/// <summary>
		/// The ID of the action bar action.
		/// Could be the spell id or the item id.
		/// Depends on <see cref="Type"/>.
		/// </summary>
		[JsonProperty]
		public int ActionId { get; private set; }

		public CharacterActionBarInstanceModel(ActionBarIndex barIndex, FreecraftCore.ActionButtonType type, int actionId)
		{
			if (!Enum.IsDefined(typeof(ActionBarIndex), barIndex)) throw new InvalidEnumArgumentException(nameof(barIndex), (int) barIndex, typeof(ActionBarIndex));
			if (!Enum.IsDefined(typeof(FreecraftCore.ActionButtonType), type)) throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(FreecraftCore.ActionButtonType));
			if (actionId < 0) throw new ArgumentOutOfRangeException(nameof(actionId));

			BarIndex = barIndex;
			Type = type;
			ActionId = actionId;
		}

		public static CharacterActionBarInstanceModel CreateItemAction(ActionBarIndex barIndex, int actionId)
		{
			if (!Enum.IsDefined(typeof(ActionBarIndex), barIndex)) throw new InvalidEnumArgumentException(nameof(barIndex), (int) barIndex, typeof(ActionBarIndex));
			if (actionId < 0) throw new ArgumentOutOfRangeException(nameof(actionId));
			
			return new CharacterActionBarInstanceModel(barIndex, FreecraftCore.ActionButtonType.ACTION_BUTTON_ITEM, actionId);
		}

		public static CharacterActionBarInstanceModel CreateSpellAction(ActionBarIndex barIndex, int actionId)
		{
			if(!Enum.IsDefined(typeof(ActionBarIndex), barIndex)) throw new InvalidEnumArgumentException(nameof(barIndex), (int)barIndex, typeof(ActionBarIndex));
			if(actionId < 0) throw new ArgumentOutOfRangeException(nameof(actionId));

			return new CharacterActionBarInstanceModel(barIndex, FreecraftCore.ActionButtonType.ACTION_BUTTON_SPELL, actionId);
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		internal CharacterActionBarInstanceModel()
		{
			
		}
	}
}
