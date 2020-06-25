using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public interface IActionBarMouseOverStateChangeEventSubscribable
	{
		event EventHandler<ActionBarMouseOverStateChangeEventArgs> OnActionBarMouseOverStateChanged;
	}

	public sealed class ActionBarMouseOverStateChangeEventArgs : EventArgs
	{
		/// <summary>
		/// The ID of the action.
		/// </summary>
		public int ActionId { get; private set; }

		/// <summary>
		/// The action bar type.
		/// </summary>
		public ActionButtonType ActionType { get; private set; }

		/// <summary>
		/// Indicates if the action bar icon state is moused over.
		/// </summary>
		public bool IsMousedOver { get; private set; }

		public ActionBarMouseOverStateChangeEventArgs(int actionId, ActionButtonType actionType, bool isMousedOver)
		{
			if (!Enum.IsDefined(typeof(ActionButtonType), actionType)) throw new InvalidEnumArgumentException(nameof(actionType), (int) actionType, typeof(ActionButtonType));
			if (actionId < 0) throw new ArgumentOutOfRangeException(nameof(actionId));

			ActionId = actionId;
			ActionType = actionType;
			IsMousedOver = isMousedOver;
		}
	}
}
