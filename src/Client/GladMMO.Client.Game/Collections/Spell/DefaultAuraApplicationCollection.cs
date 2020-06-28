using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public sealed class DefaultAuraApplicationCollection : IAuraApplicationCollection
	{
		private static ClientAuraApplicationData[] Empty { get; } = new ClientAuraApplicationData[byte.MaxValue];

		private ClientAuraApplicationData[] InternalAuraApplicationMap { get; } = new ClientAuraApplicationData[byte.MaxValue];

		static DefaultAuraApplicationCollection()
		{
			for(byte i = 0; i < byte.MaxValue; i++)
				Empty[i] = new ClientAuraApplicationData(0, new AuraUpdateData(i));
		}

		public bool IsSlotActive(byte auraSlot)
		{
			return InternalAuraApplicationMap[auraSlot] != null && !InternalAuraApplicationMap[auraSlot].Data.IsAuraRemoved;
		}

		public ClientAuraApplicationData this[byte slot]
		{
			get
			{
				if (InternalAuraApplicationMap[slot] == null)
					return Empty[slot];
				else
					return InternalAuraApplicationMap[slot];
			}
		}

		public void Apply([NotNull] ClientAuraApplicationData data)
		{
			if (data == null) throw new ArgumentNullException(nameof(data));

			InternalAuraApplicationMap[data.Data.SlotIndex] = data;
		}

		public void Remove([NotNull] ClientAuraApplicationData data)
		{
			if (data == null) throw new ArgumentNullException(nameof(data));

			InternalAuraApplicationMap[data.Data.SlotIndex] = null;
		}

		public void Update([NotNull] ClientAuraApplicationData data)
		{
			if (data == null) throw new ArgumentNullException(nameof(data));

			if (!IsSlotActive(data.Data.SlotIndex))
				throw new InvalidOperationException($"Cannot {nameof(Update)} Slot: {data.Data.SlotIndex} because the slot is not already active.");

			InternalAuraApplicationMap[data.Data.SlotIndex] = data;
		}

		public void Remove(byte slot)
		{
			InternalAuraApplicationMap[slot] = null;
		}

		public IEnumerator<ClientAuraApplicationData> GetEnumerator()
		{
			//Only return non-null valid applications.
			foreach(var entry in InternalAuraApplicationMap)
				if (entry != null && !entry.Data.IsAuraRemoved)
					yield return entry;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
