using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IGeneralErrorEncounteredEventSubscribable
	{
		event EventHandler<GeneralErrorEncounteredEventArgs> OnErrorEncountered;
	}

	public sealed class GeneralErrorEncounteredEventArgs : EventArgs
	{
		public string ErrorTitle { get; }

		public string ErrorMessage { get; }

		/// <summary>
		/// Optional action to take when the Ok button is clicked.
		/// </summary>
		[CanBeNull]
		public Action OnButtonClicked { get; }

		public GeneralErrorEncounteredEventArgs([NotNull] string errorTitle, [NotNull] string errorMessage, [CanBeNull] Action buttonClicked)
		{
			if (string.IsNullOrEmpty(errorTitle)) throw new ArgumentException("Value cannot be null or empty.", nameof(errorTitle));
			if (string.IsNullOrEmpty(errorMessage)) throw new ArgumentException("Value cannot be null or empty.", nameof(errorMessage));

			ErrorTitle = errorTitle;
			ErrorMessage = errorMessage;
			OnButtonClicked = buttonClicked;
		}
	}
}
