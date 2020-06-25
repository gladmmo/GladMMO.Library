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

		void Awake()
		{
			TooltipTextMap.Add(SpellTooltipInfoSlot.Name, TextCollection.SpellName);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Rank, TextCollection.SpellRank);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Cost, TextCollection.SpellCost);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Range, TextCollection.SpellRange);
			TooltipTextMap.Add(SpellTooltipInfoSlot.CastTime, TextCollection.SpellCastTime);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Cooldown, TextCollection.SpellCooldown);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Requirements, TextCollection.SpellRequirements);
			TooltipTextMap.Add(SpellTooltipInfoSlot.Info, TextCollection.SpellInfo);
		}

		public void SetText(SpellTooltipInfoSlot slot, string text)
		{
			TooltipTextMap[slot].text = text;
		}

		public void SetSlotState(SpellTooltipInfoSlot slot, bool state)
		{
			TooltipTextMap[slot].gameObject.SetActive(state);
		}

		public bool IsSlotActive(SpellTooltipInfoSlot slot)
		{
			return TooltipTextMap[slot].gameObject.activeSelf;
		}

		public bool SetActive(bool state)
		{
			gameObject.SetActive(state);
			return gameObject.activeSelf;
		}
	}
}
