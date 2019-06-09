using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.FinalIK
{
	public interface IAvatarIKReferenceContainer<out TReferenceType>
		where TReferenceType : IInverseKinematicReferenceable
	{
		//The reason this is lowercase is for compatibility with
		//FinalIK. It uses a lot of public fields/props with lowercases.
		TReferenceType references { get; }
	}
}
