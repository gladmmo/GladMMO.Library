using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IMovementDirectionSettable
	{
		Vector2 Direction { get; set; }
	}
}
