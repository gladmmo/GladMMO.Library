using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IGameObjectEntityBehaviourFactory : IFactoryCreatable<BaseGameObjectEntityBehaviourComponent, NetworkEntityGuid>
	{

	}
}
