using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Database table for icon entries.
	/// </summary>
	[Table("clientcontent_icon")]
	public class ContentIconEntryModel : IDatabaseModelKeyable, IContentIconEntry
	{
		[NotMapped]
		public int EntryKey => IconId;

		/// <summary>
		/// The unique identifier for the icon entry.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IconId { get; private set; }

		/// <summary>
		/// The relative path name WITH extension.
		/// </summary>
		[NotNull]
		[Required]
		public string IconPathName { get; private set; }

		/// <summary>
		/// Icon path without the extension.
		/// </summary>
		[NotMapped]
		public string IconPathNameWithoutExtensions => Path.GetFileNameWithoutExtension(IconPathName);

		public ContentIconEntryModel([JetBrains.Annotations.NotNull] string iconPathName)
		{
			if (string.IsNullOrWhiteSpace(iconPathName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(iconPathName));

			IconPathName = iconPathName;
		}

		/// <summary>
		/// EF Ctor.
		/// </summary>
		internal ContentIconEntryModel()
		{

		}
	}
}
