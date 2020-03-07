using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IMovementDirectionChangedListener
	{
		void SetMovementDirection(Vector2 direction);
	}
}
