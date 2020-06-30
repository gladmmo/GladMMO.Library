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

		[SerializeField]
		private GameObject AuraDebuffPrefab;

		[SerializeField]
		private Transform NegativeAuraBarTransform;

		private Queue<IUIAuraBuffSlot> PositiveBuffSlotPool { get; } = new Queue<IUIAuraBuffSlot>(255);

		private Queue<IUIAuraBuffSlot> NegativeBuffSlotPool { get; } = new Queue<IUIAuraBuffSlot>(255);

		private Dictionary<byte, IUIAuraBuffSlot> InternalAuraMap { get; } = new Dictionary<byte, IUIAuraBuffSlot>(byte.MaxValue);

		public IUIAuraBuffSlot this[AuraBuffType type, byte index] => RetrieveUIAuraSlot(type, index);

		public event EventHandler<AuraBuffClickedEventArgs> OnAuraBuffClicked;

		public IEnumerable<IUIAuraBuffSlot> EnumerateActive()
		{
			foreach(var slot in InternalAuraMap.Values)
				if (slot != null && slot.RootElement.isActive)
					yield return slot;
		}

		private IUIAuraBuffSlot RetrieveUIAuraSlot(AuraBuffType type, byte index)
		{
			if (!Enum.IsDefined(typeof(AuraBuffType), type)) throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(AuraBuffType));

			//Return only ACTIVE one.
			if (InternalAuraMap.ContainsKey(index))
			{
				IUIAuraBuffSlot slot = InternalAuraMap[index];

				if(slot.RootElement.isActive)
					return slot;
				else
				{
					if (type == slot.BuffType)
						return slot;
					else
						switch (slot.BuffType)
						{
							case AuraBuffType.Positive:
								PositiveBuffSlotPool.Enqueue(slot);
								break;
							case AuraBuffType.Negative:
								NegativeBuffSlotPool.Enqueue(slot);
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}

					InternalAuraMap[index] = null;
				}
			}

			if(type == AuraBuffType.Positive)
			{
				return GetPositiveBuffSlot(index);
			}
			else
			{
				return GetNegativeBuffSlot(index);
			}
		}

		private IUIAuraBuffSlot GetNegativeBuffSlot(byte index)
		{
			if (NegativeBuffSlotPool.Count == 0)
			{
				GameObject auraSlot = Instantiate(AuraDebuffPrefab, Vector3.zero, Quaternion.identity, NegativeAuraBarTransform);
				return InternalAuraMap[index] = GetAuraComponent(auraSlot);
			}
			else
			{
				return InternalAuraMap[index] = NegativeBuffSlotPool.Dequeue();
			}
		}

		private IUIAuraBuffSlot GetPositiveBuffSlot(byte index)
		{
			if (PositiveBuffSlotPool.Count == 0)
			{
				GameObject auraSlot = Instantiate(AuraBuffPrefab, Vector3.zero, Quaternion.identity, PositiveAuraBarTransform);
				return InternalAuraMap[index] = GetAuraComponent(auraSlot);
			}
			else
			{
				return InternalAuraMap[index] = PositiveBuffSlotPool.Dequeue();
			}
		}

		private AuraBuffSlotUIAdapter GetAuraComponent(GameObject auraSlot)
		{
			AuraBuffSlotUIAdapter adapter = auraSlot.GetComponent<AuraBuffSlotUIAdapter>();

			//Positive buffs can be DEBUFFED!
			if (adapter.BuffType == AuraBuffType.Positive)
			{
				adapter.AuraButton.AddOnClickListener(() => OnAuraButtonClicked(adapter));
			}

			return adapter;
		}

		private void OnAuraButtonClicked(IUIAuraBuffSlot slot)
		{
			//This is dumb and slow, but it ALMOST never happens
			//so performance doesn't matter.
			foreach (var kvp in InternalAuraMap)
			{
				//VERY dumb, to do it via reference but the original
				//aura collection design FORCES this
				if (kvp.Value == slot)
				{
					//TODO: THIS IS A TOTAL HACK!! This is NOT how we should do right-clicking events
					OnAuraBuffClicked?.Invoke(this, new AuraBuffClickedEventArgs(kvp.Key));
					break;
				}
			}
		}
	}
}
