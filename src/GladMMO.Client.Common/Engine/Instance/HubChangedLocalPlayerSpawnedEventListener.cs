using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class HubChangedLocalPlayerSpawnedEventListener : OnLocalPlayerSpawnedEventListener
	{
		private IEntityDataChangeCallbackRegisterable EntityDataCallbackRegister { get; }

		/// <summary>
		/// The local player's details
		/// </summary>
		protected IReadonlyLocalPlayerDetails PlayerDetails { get; }

		/// <inheritdoc />
		protected HubChangedLocalPlayerSpawnedEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] IEntityDataChangeCallbackRegisterable entityDataCallbackRegister,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails)
			: base(subscriptionService)
		{
			EntityDataCallbackRegister = entityDataCallbackRegister ?? throw new ArgumentNullException(nameof(entityDataCallbackRegister));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected void RegisterPlayerDataChangeCallback<TChangeType>(EUnitFields field, [NotNull] Action<NetworkEntityGuid, EntityDataChangedArgs<TChangeType>> callback)
			where TChangeType : struct
		{
			if(callback == null) throw new ArgumentNullException(nameof(callback));
			if(!Enum.IsDefined(typeof(EUnitFields), field)) throw new InvalidEnumArgumentException(nameof(field), (int)field, typeof(EUnitFields));

			EntityDataCallbackRegister.RegisterCallback(PlayerDetails.LocalPlayerGuid, (int)field, callback);
		}
	}
}
