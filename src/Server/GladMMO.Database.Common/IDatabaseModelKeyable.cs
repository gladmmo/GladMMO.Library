using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	public interface IDatabaseModelKeyable
	{
		/// <summary>
		/// The key to the database model entry.
		/// </summary>
		[NotMapped]
		int EntryKey { get; }
	}
}
