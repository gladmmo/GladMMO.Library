using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IClientGameObjectEntityBehaviourFactory : IFactoryCreatable<BaseEntityBehaviourComponent, NetworkEntityGuid>
	{

	}
}
