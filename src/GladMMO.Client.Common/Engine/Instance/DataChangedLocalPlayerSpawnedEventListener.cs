using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Generic.Math;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class DataChangedLocalPlayerSpawnedEventListener : OnLocalPlayerSpawnedEventListener
	{
		private IEntityDataChangeCallbackRegisterable EntityDataCallbackRegister { get; }

		/// <summary>
		/// The local player's details
		/// </summary>
		protected IReadonlyLocalPlayerDetails PlayerDetails { get; }

		/// <inheritdoc />
		protected DataChangedLocalPlayerSpawnedEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] IEntityDataChangeCallbackRegisterable entityDataCallbackRegister,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails)
			: base(subscriptionService)
		{
			EntityDataCallbackRegister = entityDataCallbackRegister ?? throw new ArgumentNullException(nameof(entityDataCallbackRegister));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected void RegisterPlayerDataChangeCallback<TChangeType>(int field, [NotNull] Action<NetworkEntityGuid, EntityDataChangedArgs<TChangeType>> callback)
			where TChangeType : struct
		{
			if(callback == null) throw new ArgumentNullException(nameof(callback));

			EntityDataCallbackRegister.RegisterCallback(PlayerDetails.LocalPlayerGuid, (int)field, callback);
		}

		protected void RegisterPlayerDataChangeCallback<TChangeType>(Enum field, [NotNull] Action<NetworkEntityGuid, EntityDataChangedArgs<TChangeType>> callback)
			where TChangeType : struct
		{
			if(callback == null) throw new ArgumentNullException(nameof(callback));

			EntityDataCallbackRegister.RegisterCallback(PlayerDetails.LocalPlayerGuid, Unsafe.As<Enum, int>(ref field), callback);
		}

		protected void RegisterPlayerDataChangeCallback<TChangeType>(BaseObjectField field, [NotNull] Action<NetworkEntityGuid, EntityDataChangedArgs<TChangeType>> callback)
			where TChangeType : struct
		{
			if(callback == null) throw new ArgumentNullException(nameof(callback));

			EntityDataCallbackRegister.RegisterCallback(PlayerDetails.LocalPlayerGuid, (int)field, callback);
		}
	}
}
