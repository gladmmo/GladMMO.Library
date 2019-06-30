using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CreatureTemplateCollectionModel
	{
		[JsonProperty(PropertyName = "Templates")]
		private CreatureTemplateModel[] _Templates { get; set; }

		[JsonIgnore]
		public IReadOnlyCollection<CreatureTemplateModel> Templates => _Templates;

		/// <summary>
		/// Creatures a Successful <see cref="CreatureTemplateCollectionModel"/>
		/// </summary>
		/// <param name="templates">The entries to send.</param>
		public CreatureTemplateCollectionModel([NotNull] CreatureTemplateModel[] templates)
		{
			if(templates == null) throw new ArgumentNullException(nameof(templates));
			if(templates.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(templates));

			_Templates = templates;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected CreatureTemplateCollectionModel()
		{
			
		}
	}
}
