using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class ChangeEntityActorDisplayModelMessage : EntityActorMessage
	{
		/// <summary>
		/// The new model/display ID to be set.
		/// </summary>
		public int NewModelId { get; }

		public ChangeEntityActorDisplayModelMessage(int newModelId)
		{
			if (newModelId <= 0) throw new ArgumentOutOfRangeException(nameof(newModelId));

			NewModelId = newModelId;
		}
	}
}
