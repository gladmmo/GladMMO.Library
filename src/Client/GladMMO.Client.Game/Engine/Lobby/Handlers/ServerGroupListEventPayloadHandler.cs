using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	/*[AdditionalRegisterationAs(typeof(IPlayerGroupLeftEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IPlayerGroupJoinedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.DefaultLobby)]
	public sealed class ServerGroupListEventPayloadHandler : BaseGameClientGameMessageHandler<ServerGroupListEvent>, IPlayerGroupJoinedEventSubscribable, IPlayerGroupLeftEventSubscribable
	{
		private HashSet<NetworkEntityGuid> GroupedPlayerSet { get; }

		private Dictionary<NetworkEntityGuid, GroupListMemberData> GroupMemberDataDictionary { get; }

		/// <inheritdoc />
		public event EventHandler<PlayerJoinedGroupEventArgs> OnPlayerJoinedGroup;

		/// <inheritdoc />
		public event EventHandler<PlayerLeftGroupEventArgs> OnPlayerLeftGroup;

		/// <inheritdoc />
		public ServerGroupListEventPayloadHandler(ILog logger)
			: base(logger)
		{
			GroupedPlayerSet = new HashSet<NetworkEntityGuid>(NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance);
			GroupMemberDataDictionary = new Dictionary<NetworkEntityGuid, GroupListMemberData>(NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance);
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, ServerGroupListEvent payload)
		{
			if(Logger.IsDebugEnabled)
			{
				if(payload.isGroupHaveAnyMembers)
					foreach(var m in payload.GroupMemberDataList)
						if(Logger.IsDebugEnabled)
							Logger.Debug($"GroupMember GroupId: {m.GroupId} Flags: {m.MemberFlags} Status: {m.MemberStatus} Roles: {m.OptionalDungeonLFGRoles} Guid: {m.PlayerGuid} Name: {m.PlayerName}");
			}

			//Groups are like sets so we can do some
			//set operations to find out the NEW players!
			if(payload.isGroupHaveAnyMembers)
			{
				ImmutableHashSet<NetworkEntityGuid> groupListEventHashSet = payload.GroupMemberDataList.Select(g => g.PlayerGuid).ToImmutableHashSet(NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance);

				//WoW send an update on every change, which is dumb, but we shouldn't assume it works that way always.
				//group data dictionary, we need this for information to broadcast the events
				//AND we need to actually maintain this data
				var groupDataDictionary = payload.GroupMemberDataList.ToDictionary(data => data.PlayerGuid, NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance);

				foreach(NetworkEntityGuid newPlayer in groupListEventHashSet.Except(GroupedPlayerSet))
				{
					HandlePlayerJoined(newPlayer, groupDataDictionary);
				}

				//Now we need to know who left the group
				foreach(NetworkEntityGuid playerLeaving in GroupedPlayerSet.Except(groupListEventHashSet))
					HandlePlayerRemoved(playerLeaving);
			}
			else if(GroupedPlayerSet.Any())
			{
				//If the group update has no memembers, and the group set has players
				//then that means we are no longer in a group at all somehow.

				//So this case is the simple case, broadcast the removal of all players.
				foreach(NetworkEntityGuid playerLeaving in GroupedPlayerSet)
					HandlePlayerRemoved(playerLeaving, false);

				GroupedPlayerSet.Clear();
			}
			else
			{
				if(Logger.IsInfoEnabled)
					Logger.Warn($"Encountered Group Status Update when no members are contained in the update and we don't currently know about any group members.");
			}

			return Task.CompletedTask;
		}

		private void HandlePlayerJoined([NotNull] NetworkEntityGuid newPlayer, [NotNull] Dictionary<NetworkEntityGuid, GroupListMemberData> currentPlayerDataDictionary)
		{
			if(newPlayer == null) throw new ArgumentNullException(nameof(newPlayer));
			if(currentPlayerDataDictionary == null) throw new ArgumentNullException(nameof(currentPlayerDataDictionary));

			OnPlayerJoinedGroup?.Invoke(this, new PlayerJoinedGroupEventArgs(newPlayer));
			GroupMemberDataDictionary.Add(newPlayer, currentPlayerDataDictionary[newPlayer]);
			GroupedPlayerSet.Add(newPlayer);

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Player: {currentPlayerDataDictionary[newPlayer].PlayerName} joined the group.");
		}

		private void HandlePlayerRemoved([NotNull] NetworkEntityGuid playerLeaving, bool handleRemovingFromGroupSet = true)
		{
			if(playerLeaving == null) throw new ArgumentNullException(nameof(playerLeaving));

			OnPlayerLeftGroup?.Invoke(this, new PlayerLeftGroupEventArgs(playerLeaving));

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Player: {GroupMemberDataDictionary[playerLeaving].PlayerName} left the group.");

			GroupMemberDataDictionary.Remove(playerLeaving);

			if(handleRemovingFromGroupSet)
				GroupedPlayerSet.Remove(playerLeaving);
		}
	}*/
}
