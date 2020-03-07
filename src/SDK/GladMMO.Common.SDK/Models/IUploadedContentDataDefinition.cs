using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.SDK
{
	/// <summary>
	/// Contract for types that represent data for an uploadable content.
	/// </summary>
	public interface IUploadedContentDataDefinition
	{
		/// <summary>
		/// The unique 64bit identifier for the content.
		/// </summary>
		long ContentId { get; set; } //These have to be mutable now... OH well.

		/// <summary>
		/// The unique 64bit content name identifier for the stored content.
		/// </summary>
		Guid ContentGuid { get; set; } //These have to be mutable now... OH well.
	}
}
