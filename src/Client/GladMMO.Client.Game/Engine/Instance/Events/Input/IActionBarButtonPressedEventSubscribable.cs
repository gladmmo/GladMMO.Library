using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IActionBarButtonPressedEventSubscribable
	{
		event EventHandler<ActionBarButtonPressedEventArgs> OnActionBarButtonPressed;
	}

	public sealed class ActionBarButtonPressedEventArgs : EventArgs
	{
		public ActionBarIndex Index { get; }

		public ActionBarButtonPressedEventArgs(ActionBarIndex index)
		{
			if (!Enum.IsDefined(typeof(ActionBarIndex), index)) throw new InvalidEnumArgumentException(nameof(index), (int) index, typeof(ActionBarIndex));

			Index = index;
		}
	}
}
