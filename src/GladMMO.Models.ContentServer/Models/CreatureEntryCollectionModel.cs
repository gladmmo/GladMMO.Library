using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CreatureEntryCollectionModel
	{
		[JsonProperty(PropertyName = "Entries")]
		private CreatureInstanceModel[] _Entries { get; set; }

		[JsonIgnore]
		public IReadOnlyCollection<CreatureInstanceModel> Entries => _Entries;

		/// <summary>
		/// Creatures a Successful <see cref="CreatureEntryCollectionModel"/>
		/// </summary>
		/// <param name="entries">The entries to send.</param>
		public CreatureEntryCollectionModel([NotNull] CreatureInstanceModel[] entries)
		{
			if(entries == null) throw new ArgumentNullException(nameof(entries));
			if(entries.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(entries));

			_Entries = entries;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected CreatureEntryCollectionModel()
		{
			
		}
	}
}
