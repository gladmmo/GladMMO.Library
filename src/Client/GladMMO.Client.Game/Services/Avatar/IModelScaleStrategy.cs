using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	/// <summary>
	/// Contract for type that can compute an object's scale.
	/// </summary>
	public interface IModelScaleStrategy
	{
		float ComputeScale(ObjectGuid guid);
	}
}
