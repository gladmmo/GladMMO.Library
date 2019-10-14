using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IGeneralErrorEncounteredEventListener
	{
		event EventHandler<GeneralErrorEncounteredEventArgs> OnErrorEncountered;
	}

	public sealed class GeneralErrorEncounteredEventArgs : EventArgs
	{
		public string ErrorTitle { get; }

		public string ErrorMessage { get; }

		public GeneralErrorEncounteredEventArgs([NotNull] string errorTitle, [NotNull] string errorMessage)
		{
			if (string.IsNullOrEmpty(errorTitle)) throw new ArgumentException("Value cannot be null or empty.", nameof(errorTitle));
			if (string.IsNullOrEmpty(errorMessage)) throw new ArgumentException("Value cannot be null or empty.", nameof(errorMessage));

			ErrorTitle = errorTitle;
			ErrorMessage = errorMessage;
		}
	}
}
