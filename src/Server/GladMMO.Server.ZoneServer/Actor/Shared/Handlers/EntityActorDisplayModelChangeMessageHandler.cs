using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityActorDisplayModelChangeMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, ChangeEntityActorDisplayModelMessage>
	{
		private ILog Logger { get; }

		public EntityActorDisplayModelChangeMessageHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, ChangeEntityActorDisplayModelMessage message)
		{
			//Basically we
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Entity: {state.EntityGuid} changing to ModelId: {message.NewModelId}");

			//We just set the model id, it's fine as simple as this.
			state.EntityData.SetFieldValue(BaseObjectField.UNIT_FIELD_DISPLAYID, message.NewModelId);
		}
	}
}
