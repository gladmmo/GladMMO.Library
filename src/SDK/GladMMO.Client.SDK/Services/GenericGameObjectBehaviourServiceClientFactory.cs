using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using GladMMO.SDK;

namespace GladMMO
{
	public interface IGameObjectBehaviourDataClientFactory<TBehaviourType> : IFactoryCreatable<IGameObjectBehaviourDataServiceClient<TBehaviourType>, EmptyFactoryContext>
		where TBehaviourType : class
	{

	}

	public abstract class GenericGameObjectBehaviourServiceClientFactory<TBehaviourType> : IGameObjectBehaviourDataClientFactory<TBehaviourType> 
		where TBehaviourType : class
	{
		public abstract IGameObjectBehaviourDataServiceClient<TBehaviourType> Create(EmptyFactoryContext context);
	}
}
