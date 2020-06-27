using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public sealed class DefaultAuraApplicationCollection : IAuraApplicationCollection
	{
		private static AuraUpdateData[] Empty { get; } = new AuraUpdateData[byte.MaxValue];

		private AuraUpdateData[] InternalAuraApplicationMap { get; } = new AuraUpdateData[byte.MaxValue];

		static DefaultAuraApplicationCollection()
		{
			for(byte i = 0; i < byte.MaxValue; i++)
				Empty[i] = new AuraUpdateData(i);
		}

		public bool IsSlotActive(byte auraSlot)
		{
			return InternalAuraApplicationMap[auraSlot] != null && !InternalAuraApplicationMap[auraSlot].IsAuraRemoved;
		}

		public AuraUpdateData this[byte slot]
		{
			get
			{
				if (InternalAuraApplicationMap[slot] == null)
					return Empty[slot];
				else
					return InternalAuraApplicationMap[slot];
			}
		}

		public void Apply(AuraUpdateData data)
		{
			InternalAuraApplicationMap[data.SlotIndex] = data;
		}

		public void Remove(AuraUpdateData data)
		{
			InternalAuraApplicationMap[data.SlotIndex] = null;
		}

		public void Update([NotNull] AuraUpdateData data)
		{
			if (data == null) throw new ArgumentNullException(nameof(data));

			if (!IsSlotActive(data.SlotIndex))
				throw new InvalidOperationException($"Cannot {nameof(Update)} Slot: {data.SlotIndex} because the slot is not already active.");

			InternalAuraApplicationMap[data.SlotIndex] = data;
		}

		public void Remove(byte slot)
		{
			InternalAuraApplicationMap[slot] = null;
		}

		public IEnumerator<AuraUpdateData> GetEnumerator()
		{
			//Only return non-null valid applications.
			foreach(var entry in InternalAuraApplicationMap)
				if (entry != null && !entry.IsAuraRemoved)
					yield return entry;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
