using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IButtonClickedEventSubscribable
	{
		event EventHandler<ButtonClickedEventArgs> OnButtonClicked;
	}

	public sealed class ButtonClickedEventArgs : EventArgs
	{
		public IUIButton Button { get; }

		public ButtonClickedEventArgs([NotNull] IUIButton button)
		{
			Button = button ?? throw new ArgumentNullException(nameof(button));
		}
	}
}
