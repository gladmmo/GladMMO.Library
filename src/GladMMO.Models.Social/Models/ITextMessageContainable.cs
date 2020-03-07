using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that can contain
	/// a text message.
	/// </summary>
	public interface ITextMessageContainable
	{
		/// <summary>
		/// The chat message intended to be sent.
		/// </summary>
		[JsonProperty]
		string Message { get; }
	}
}
