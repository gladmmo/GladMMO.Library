using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class InstanceObjectEntryCollectionModel<TModelType>
	{
		[JsonProperty(PropertyName = "Entries")]
		private TModelType[] _Entries { get; set; }

		[JsonIgnore]
		public IReadOnlyCollection<TModelType> Entries => _Entries;

		/// <summary>
		/// Creatures a Successful <see cref="InstanceObjectEntryCollectionModel{TModelType}"/>
		/// </summary>
		/// <param name="entries">The entries to send.</param>
		public InstanceObjectEntryCollectionModel([NotNull] TModelType[] entries)
		{
			if(entries == null) throw new ArgumentNullException(nameof(entries));
			if(entries.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(entries));

			_Entries = entries;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected InstanceObjectEntryCollectionModel()
		{
			
		}
	}
}
