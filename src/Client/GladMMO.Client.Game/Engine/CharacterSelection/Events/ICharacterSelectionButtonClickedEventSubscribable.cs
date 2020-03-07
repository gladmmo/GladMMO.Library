using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICharacterSelectionButtonClickedEventSubscribable
	{
		event EventHandler<CharacterButtonClickedEventArgs> OnCharacterButtonClicked;
	}

	public sealed class CharacterButtonClickedEventArgs : EventArgs
	{
		public ObjectGuid CharacterGuid { get; }

		public int ButtonSlot { get; }

		/// <inheritdoc />
		public CharacterButtonClickedEventArgs([NotNull] ObjectGuid characterGuid, int buttonSlot)
		{
			if(buttonSlot < 0) throw new ArgumentOutOfRangeException(nameof(buttonSlot));

			CharacterGuid = characterGuid ?? throw new ArgumentNullException(nameof(characterGuid));
			ButtonSlot = buttonSlot;
		}
	}
}
