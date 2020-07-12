using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class QuestRewardModel
	{
		/// <summary>
		/// The experience rewarded.
		/// </summary>
		public int Experience { get; internal set; }

		/// <summary>
		/// The money rewarded.
		/// </summary>
		public int Money { get; internal set; }

		public QuestRewardModel(int experience, int money)
		{
			Experience = experience;
			Money = money;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		internal QuestRewardModel()
		{
			
		}
	}
}
