using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.UI;

namespace GladMMO
{
	[Serializable]
	public struct TooltipTextCollection
	{
		[SerializeField]
		public UnityEngine.UI.Text SpellName;

		[SerializeField]
		public UnityEngine.UI.Text SpellRank;

		[SerializeField]
		public UnityEngine.UI.Text SpellCost;

		[SerializeField]
		public UnityEngine.UI.Text SpellRange;

		[SerializeField]
		public UnityEngine.UI.Text SpellCastTime;

		[SerializeField]
		public UnityEngine.UI.Text SpellCooldown;

		[SerializeField]
		public UnityEngine.UI.Text SpellRequirements;

		[SerializeField]
		public UnityEngine.UI.Text SpellInfo;
	}

	public sealed class SpellToolTipAdapter : BaseUnityUI<IUISpellTooltip>, IUISpellTooltip
	{
		// ReSharper disable once FieldCanBeMadeReadOnly.Local
		[SerializeField]
		private TooltipTextCollection TextCollection = new TooltipTextCollection();

		private Dictionary<SpellTooltipInfoSlot, UnityEngine.UI.Text> TooltipTextMap { get; } = new Dictionary<SpellTooltipInfoSlot, Text>();

		private bool isInitialized { get; set; } = false;

		private void Awake()
		{
			if (isInitialized)
				return;

			TooltipTextMap.Add(SpellTooltipInfoSlot.Name, TextCollection.SpellName);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Rank, TextCollection.SpellRank);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Cost, TextCollection.SpellCost);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Range, TextCollection.SpellRange);
			TooltipTextMap.Add(SpellTooltipInfoSlot.CastTime, TextCollection.SpellCastTime);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Cooldown, TextCollection.SpellCooldown);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Requirements, TextCollection.SpellRequirements);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Info, TextCollection.SpellInfo);

			isInitialized = true;
		}

		public void SetText(SpellTooltipInfoSlot slot, string text)
		{
			//Since the component may not be activate, we must ensure awake was called.
			AssertInitialized();

			TooltipTextMap[slot].text = text;
		}

		public void SetSlotState(SpellTooltipInfoSlot slot, bool state)
		{
			//Since the component may not be activate, we must ensure awake was called.
			AssertInitialized();

			TooltipTextMap[slot].gameObject.SetActive(state);
		}

		public bool IsSlotActive(SpellTooltipInfoSlot slot)
		{
			//Since the component may not be activate, we must ensure awake was called.
			AssertInitialized();

			return TooltipTextMap[slot].gameObject.activeSelf;
		}

		public bool SetActive(bool state)
		{
			//Since the component may not be activate, we must ensure awake was called.
			AssertInitialized();

			gameObject.SetActive(state);
			return gameObject.activeSelf;
		}

		private void AssertInitialized()
		{
			if (!isInitialized)
				Awake();
		}
	}
}
