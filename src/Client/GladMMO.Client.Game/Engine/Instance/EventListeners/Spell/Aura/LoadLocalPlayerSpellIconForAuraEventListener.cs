using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using FreecraftCore;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LoadLocalPlayerSpellIconForAuraEventListener : BaseSingleEventListenerInitializable<IAuraApplicationAppliedEventSubscribable, AuraApplicationAppliedEventArgs>
	{
		private IAddressableContentLoader ContentLoadService { get; }

		private IUIAuraBuffCollection AuraBuffUICollection { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private IClientDataCollectionContainer ClientData { get; }

		private IReadonlyEntityGuidMappable<IAuraApplicationCollection> AuraApplicationMappable { get; }

		public LoadLocalPlayerSpellIconForAuraEventListener([NotNull] IAuraApplicationAppliedEventSubscribable subscriptionService,
			[NotNull] IAddressableContentLoader contentLoadService,
			[KeyFilter(UnityUIRegisterationKey.LocalPlayerAuraBuffCollection)] [NotNull] IUIAuraBuffCollection auraBuffUiCollection,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IClientDataCollectionContainer clientData,
			[NotNull] IReadonlyEntityGuidMappable<IAuraApplicationCollection> auraApplicationMappable) 
			: base(subscriptionService)
		{
			ContentLoadService = contentLoadService ?? throw new ArgumentNullException(nameof(contentLoadService));
			AuraBuffUICollection = auraBuffUiCollection ?? throw new ArgumentNullException(nameof(auraBuffUiCollection));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			AuraApplicationMappable = auraApplicationMappable ?? throw new ArgumentNullException(nameof(auraApplicationMappable));
		}

		protected override void OnEventFired(object source, AuraApplicationAppliedEventArgs args)
		{
			//Only local players.
			if (args.Target != PlayerDetails.LocalPlayerGuid)
				return;

			//Event call order doesn't matter
			IUIAuraBuffSlot slot = AuraBuffUICollection[args.ApplicationData.Flags.ToBuffType(), args.Slot];

			uint iconId = ClientData.AssertEntry<SpellEntry<string>>(args.SpellId).SpellIconID;

			SpellIconEntry<string> iconEntry = ClientData.AssertEntry<SpellIconEntry<string>>((int)iconId);
			IAuraApplicationCollection applicationCollection = AuraApplicationMappable.RetrieveEntity(args.Target);

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				Texture2D iconTexture = await ContentLoadService.LoadContentAsync<Texture2D>(iconEntry.TextureFileName);

				if (iconTexture == null)
					throw new InvalidOperationException($"Failed to load Icon with Path: {iconEntry.TextureFileName}");

				//Due to async nature, we must ensure that it's still valid operation
				if (!applicationCollection.IsSlotActive(args.Slot) || args.SpellId != applicationCollection[args.Slot].Data.AuraSpellId)
					return;

				slot.AuraIconImage.SetSpriteTexture(iconTexture);
			});
		}
	}
}
