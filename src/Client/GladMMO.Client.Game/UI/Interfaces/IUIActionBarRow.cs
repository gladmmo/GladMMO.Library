using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public class ActionBarButtonComposite
	{
		public IUIButton ActionBarButton { get; }

		public IUIImage ActionBarImageIcon { get; }

		public ActionBarButtonComposite([NotNull] IUIButton actionBarButton, [NotNull] IUIImage actionBarImageIcon)
		{
			ActionBarButton = actionBarButton ?? throw new ArgumentNullException(nameof(actionBarButton));
			ActionBarImageIcon = actionBarImageIcon ?? throw new ArgumentNullException(nameof(actionBarImageIcon));
		}
	}

	public interface IUIActionBarRow : IReadOnlyDictionary<ActionBarIndex, ActionBarButtonComposite>
	{
		/// <summary>
		/// The starting action bar index for the row.
		/// </summary>
		ActionBarIndex StartIndex { get; }

		/// <summary>
		/// The ending action bar index for the row.
		/// </summary>
		ActionBarIndex EndIndex { get; }
	}
}
