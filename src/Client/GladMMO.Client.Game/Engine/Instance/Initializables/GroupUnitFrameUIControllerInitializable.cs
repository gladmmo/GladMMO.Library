using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Controller for managing the group unit frames.
	/// </summary>
	[AdditionalRegisterationAs(typeof(IGroupUnitFrameManager))]
	[AdditionalRegisterationAs(typeof(IGroupUnitFrameIssuable))]
	[AdditionalRegisterationAs(typeof(IGroupUnitframeManagedCollection))]
	//[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class GroupUnitFrameUIControllerInitializable : IGameInitializable, IGroupUnitFrameManager
	{
		public ILog Logger { get; }

		private Stack<IUIUnitFrame> AvailableUnitFrames { get; }

		private Dictionary<ObjectGuid, ClaimedGroupUnitFrame> OwnedGroupUnitFrames { get; }

		private readonly object SyncObj = new object();

		private IEntityDataChangeCallbackRegisterable EntityCallbackRegister { get; }

		/// <inheritdoc />
		public GroupUnitFrameUIControllerInitializable([NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.GroupUnitFrames)] IReadOnlyCollection<IUIUnitFrame> groupUnitFrames,
			[NotNull] IEntityDataChangeCallbackRegisterable entityCallbackRegister)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			EntityCallbackRegister = entityCallbackRegister ?? throw new ArgumentNullException(nameof(entityCallbackRegister));
			OwnedGroupUnitFrames = new Dictionary<ObjectGuid, ClaimedGroupUnitFrame>(NetworkGuidEqualityComparer<ObjectGuid>.Instance);
			AvailableUnitFrames = new Stack<IUIUnitFrame>(groupUnitFrames.Reverse());
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			//This just sets all unitframes as disabled. We'll disable them as they are needed.
			foreach(var u in AvailableUnitFrames)
				u.SetElementActive(false);

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public GroupUnitFrameIssueResult TryClaimUnitFrame(ObjectGuid guid)
		{
			if(guid.TypeId != EntityTypeId.TYPEID_PLAYER)
				return GroupUnitFrameIssueResult.FailedNotAPlayer;

			lock(SyncObj)
			{
				if(OwnedGroupUnitFrames.ContainsKey(guid))
					return GroupUnitFrameIssueResult.FailedAlreadyClaimedUnitframe;

				if(!AvailableUnitFrames.Any())
					return GroupUnitFrameIssueResult.FailedUnitframeUnavailable;

				//Otherwise, at this point we're in a proper state to associate a unitframe
				OwnedGroupUnitFrames.Add(guid, new ClaimedGroupUnitFrame(AvailableUnitFrames.Pop()));
			}

			return GroupUnitFrameIssueResult.Success;
		}

		/// <inheritdoc />
		public GroupUnitFrameReleaseResult TryReleaseUnitFrame(ObjectGuid guid)
		{
			if(guid.TypeId != EntityTypeId.TYPEID_PLAYER)
				return GroupUnitFrameReleaseResult.FailedNotAPlayer;

			lock(SyncObj)
			{
				if(!OwnedGroupUnitFrames.ContainsKey(guid))
					return GroupUnitFrameReleaseResult.FailedNoUnitFrameClaimed;

				var current = OwnedGroupUnitFrames[guid];
				OwnedGroupUnitFrames.Remove(guid);
				current.ClearRegisteredCallbacks(); //this clears up all callbacks associated with the registeration.
				AvailableUnitFrames.Push(current.UnitFrame);
			}

			return GroupUnitFrameReleaseResult.Sucess;
		}

		/// <inheritdoc />
		public bool Contains(ObjectGuid entity)
		{
			lock(SyncObj)
				return OwnedGroupUnitFrames.ContainsKey(entity);
		}

		/// <inheritdoc />
		public IUIUnitFrame this[ObjectGuid entity]
		{
			get
			{
				lock(SyncObj)
				{
					if(!Contains(entity))
						throw new KeyNotFoundException($"Could not get {nameof(IUIUnitFrame)} for Guid: {entity.RawGuidValue}.");

					return OwnedGroupUnitFrames[entity].UnitFrame;
				}
			}
		}

		/// <inheritdoc />
		public IEntityDataEventUnregisterable RegisterCallback<TCallbackValueCastType>(ObjectGuid entity, int dataField, Action<ObjectGuid, EntityDataChangedArgs<TCallbackValueCastType>> callback) 
			where TCallbackValueCastType : struct
		{
			lock(SyncObj)
			{
				//We basically manage this stuff, since it'd suck for the enduser to have to manage queueing up these unregisterables
				//into the same collection. It would really suck for them to forget
				IEntityDataEventUnregisterable registerCallback = EntityCallbackRegister.RegisterCallback(entity, dataField, callback);

				OwnedGroupUnitFrames[entity].RegisterUnregisterableCallback(registerCallback);
				return registerCallback;
			}
		}
	}
}
