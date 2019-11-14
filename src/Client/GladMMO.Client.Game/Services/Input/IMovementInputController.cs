using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IMovementInputController
	{
		float CurrentHorizontal { get; }

		float CurrentVertical { get; }
	}
}
