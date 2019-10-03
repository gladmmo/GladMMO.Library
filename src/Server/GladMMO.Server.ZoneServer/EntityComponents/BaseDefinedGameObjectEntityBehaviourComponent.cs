using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public abstract class BaseDefinedGameObjectEntityBehaviourComponent<TBehaviourDataType> : BaseGameObjectEntityBehaviourComponent
		where TBehaviourDataType : class, IGameObjectLinkable
	{
		protected TBehaviourDataType BehaviourData { get; }

		protected BaseDefinedGameObjectEntityBehaviourComponent(NetworkEntityGuid targetEntity,
			[NotNull] GameObjectInstanceModel instanceData, 
			GameObjectTemplateModel templateData,
			[NotNull] TBehaviourDataType behaviourData) 
			: base(targetEntity, instanceData, templateData)
		{
			if (instanceData == null) throw new ArgumentNullException(nameof(instanceData));
			BehaviourData = behaviourData ?? throw new ArgumentNullException(nameof(behaviourData));

			//Don't allow invalid inputs here. Make sure the behaviour data is actually linked to the instance
			if (instanceData.Guid.EntryId != behaviourData.LinkedGameObjectId)
				throw new ArgumentException($"{nameof(BaseDefinedGameObjectEntityBehaviourComponent<TBehaviourDataType>)} constructed for Guid: {targetEntity} but provided BehaviourData of Type: {typeof(TBehaviourDataType).Name} is not linked to entity.");
		}
	}
}
