using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public abstract class BaseGameObjectEntityBehaviourComponent : BaseEntityBehaviourComponent
	{
		/// <summary>
		/// The GameObject instance specific data.
		/// </summary>
		protected GameObjectInstanceModel InstanceData { get; }

		/// <summary>
		/// The GameObject's referenced template data.
		/// </summary>
		protected GameObjectTemplateModel TemplateData { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetEntity">GameObject entity guid.</param>
		/// <param name="instanceData">GameObject instance data.</param>
		/// <param name="templateData">GameObject instance referenced template data.</param>
		protected BaseGameObjectEntityBehaviourComponent([NotNull] NetworkEntityGuid targetEntity, [NotNull] GameObjectInstanceModel instanceData, [NotNull] GameObjectTemplateModel templateData) 
			: base(targetEntity)
		{
			if (targetEntity == null) throw new ArgumentNullException(nameof(targetEntity));

			if(TargetEntity.EntityType != EntityType.GameObject)
				throw new ArgumentException($"{nameof(BaseGameObjectEntityBehaviourComponent)} constructed for Guid: {targetEntity} but was not GameObject entity.");

			if(InstanceData.TemplateId != templateData.TemplateId)
				throw new ArgumentException($"{nameof(BaseGameObjectEntityBehaviourComponent)} constructed for Guid: {targetEntity} but {InstanceData} template referenced does not match {templateData} template id.");

			InstanceData = instanceData ?? throw new ArgumentNullException(nameof(instanceData));
			TemplateData = templateData ?? throw new ArgumentNullException(nameof(templateData));
		}
	}
}
