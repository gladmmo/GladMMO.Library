using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public sealed class ClientAuraApplicationData
	{
		/// <summary>
		/// Timestamp in milliseconds of when the aura was applied.
		/// </summary>
		public int ApplicationTimeStamp { get; }

		/// <summary>
		/// The aura application data.
		/// </summary>
		public AuraUpdateData Data { get; }

		/// <summary>
		/// Creates a new client aura application data.
		/// </summary>
		/// <param name="currentTimeStamp">This should be the CURRENT remote timestamp</param>
		/// <param name="data"></param>
		public ClientAuraApplicationData(int currentTimeStamp, AuraUpdateData data)
		{
			Data = data;
			ApplicationTimeStamp = ComputeAuraStartTimeStamp(currentTimeStamp, data);
		}

		private static int ComputeAuraStartTimeStamp(int currentTimeStamp, [NotNull] AuraUpdateData data)
		{
			if (data == null) throw new ArgumentNullException(nameof(data));

			if (!data.IsAuraRemoved && data.State.HasDuration)
				return currentTimeStamp - (data.State.MaximumAuraDuration - data.State.CurrentAuraDuration);
			else
				return currentTimeStamp;
		}
	}
}
