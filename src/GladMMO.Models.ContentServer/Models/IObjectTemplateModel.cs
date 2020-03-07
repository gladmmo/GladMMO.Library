using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that are a data model
	/// for object templates.
	/// </summary>
	public interface IObjectTemplateModel
	{
		/// <summary>
		/// The unique identifier for the template
		/// the model represents.
		/// </summary>
		int TemplateId { get; }
	}
}
