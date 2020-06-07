using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public partial class CharacterAction : ICharacterActionBarEntry
	{
		[NotMapped]
		public ActionBarIndex BarIndex => (ActionBarIndex) this.Button;

		[NotMapped]
		[CanBeNull]
		public int? LinkedSpellId => ActionType == ActionButtonType.ACTION_BUTTON_SPELL ? this.ActionId : default(int?);

		[NotMapped]
		public FreecraftCore.ActionButtonType ActionType => (FreecraftCore.ActionButtonType)this.Type;

		[NotMapped]
		public int ActionId => (int) Action;
	}
}
