using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	public sealed class AuraBuffCollectionUIAdapter : BaseUnityUI<IUIAuraBuffCollection>, IUIAuraBuffCollection
	{
		[SerializeField]
		private GameObject AuraBuffPrefab;
		
		[SerializeField]
		private Transform PositiveAuraBarTransform;

		private Dictionary<byte, IUIAuraBuffSlot> InternalAuraMap { get; } = new Dictionary<byte, IUIAuraBuffSlot>(byte.MaxValue);

		public IUIAuraBuffSlot this[AuraBuffType type, byte index] => RetrieveUIAuraSlot(type, index);

		private IUIAuraBuffSlot RetrieveUIAuraSlot(AuraBuffType type, byte index)
		{
			if (!Enum.IsDefined(typeof(AuraBuffType), type)) throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(AuraBuffType));

			if (InternalAuraMap.ContainsKey(index))
				return InternalAuraMap[index];
			else
			{
				GameObject auraSlot = Instantiate(AuraBuffPrefab, Vector3.zero, Quaternion.identity, PositiveAuraBarTransform);

				return InternalAuraMap[index] = auraSlot.GetComponent<AuraBuffSlotUIAdapter>();
			}
		}
	}
}
