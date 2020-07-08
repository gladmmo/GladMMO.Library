using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class QuestTextContentModel
	{
		[JsonProperty]
		public string Title { get; internal set; }

		[JsonProperty]
		public string Details { get; internal set; }

		[JsonProperty]
		public string Objectives { get; internal set; }

		public QuestTextContentModel([NotNull] string title, [NotNull] string details, [NotNull] string objectives)
		{
			Title = title ?? throw new ArgumentNullException(nameof(title));
			Details = details ?? throw new ArgumentNullException(nameof(details));
			Objectives = objectives ?? throw new ArgumentNullException(nameof(objectives));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		internal QuestTextContentModel()
		{
			
		}
	}
}
