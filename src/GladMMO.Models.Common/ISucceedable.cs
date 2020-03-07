using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//From: https://github.com/HaloLive/HaloLive.Library/tree/master/src/HaloLive.Models.Common
	/// <summary>
	/// Contract for objects that have a successful or unsuccessful state.
	/// </summary>
	public interface ISucceedable
	{
		/// <summary>
		/// Indicates if the object represents a successful state.
		/// </summary>
		bool isSuccessful { get; }
	}
}