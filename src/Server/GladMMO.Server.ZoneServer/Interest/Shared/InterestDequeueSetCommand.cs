using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class InterestDequeueSetCommand
	{
		/// <summary>
		/// The dequeueable.
		/// </summary>
		private IEntityInterestDequeueable Dequeueable { get; }

		/// <inheritdoc />
		public InterestDequeueSetCommand([NotNull] IEntityInterestDequeueable dequeueable)
		{
			Dequeueable = dequeueable ?? throw new ArgumentNullException(nameof(dequeueable));
		}

		public void Execute()
		{
			Dequeueable.EnteringDequeueable.Clear();
			Dequeueable.LeavingDequeueable.Clear();
		}
	}
}
