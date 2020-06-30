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
	public abstract class LoadSpellIconForAuraEventListener : BaseSingleEventListenerInitializable<IAuraApplicationAppliedEventSubscribable, AuraApplicationAppliedEventArgs>
	{
		private IAddressableContentLoader ContentLoadService { get; }

		private IUIAuraBuffCollection AuraBuffUICollection { get; }

		private IClientDataCollectionContainer ClientData { get; }

		private IReadonlyEntityGuidMappable<IAuraApplicationCollection> AuraApplicationMappable { get; }

		protected LoadSpellIconForAuraEventListener([NotNull] IAuraApplicationAppliedEventSubscribable subscriptionService,
			[NotNull] IAddressableContentLoader contentLoadService,
			[NotNull] IUIAuraBuffCollection auraBuffUiCollection,
			[NotNull] IClientDataCollectionContainer clientData,
			[NotNull] IReadonlyEntityGuidMappable<IAuraApplicationCollection> auraApplicationMappable) 
			: base(subscriptionService)
		{
			ContentLoadService = contentLoadService ?? throw new ArgumentNullException(nameof(contentLoadService));
			AuraBuffUICollection = auraBuffUiCollection ?? throw new ArgumentNullException(nameof(auraBuffUiCollection));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			AuraApplicationMappable = auraApplicationMappable ?? throw new ArgumentNullException(nameof(auraApplicationMappable));
		}

		/// <summary>
		/// Abstract method that indicates if this <see cref="RemoveAppliedAuraFromUIEventListener"/> is handling aura
		/// updates from a particular Entity.
		/// </summary>
		/// <param name="target">Aura target from event.</param>
		/// <returns>True if this UI handler is handling auras for this target.</returns>
		protected abstract bool IsHandlingTarget(ObjectGuid target);

		protected override void OnEventFired(object source, AuraApplicationAppliedEventArgs args)
		{
			if (!IsHandlingTarget(args.Target))
				return;

			//Event call order doesn't matter
			IUIAuraBuffSlot slot = AuraBuffUICollection[args.ApplicationData.Flags.ToBuffType(), args.Slot];

			uint iconId = ClientData.AssertEntry<SpellEntry<string>>(args.SpellId).SpellIconID;

			SpellIconEntry<string> iconEntry = ClientData.AssertEntry<SpellIconEntry<string>>((int)iconId);
			IAuraApplicationCollection applicationCollection = AuraApplicationMappable.RetrieveEntity(args.Target);

			//TODO: For target buff icons there is a race condition that under some circumstances could cause the icon to override the correct icon
			//if a player switches between targets at the right times. Checking IsHandlingTarget won't work. MUST completely validate Handling target
			//and the spell id.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				Texture2D iconTexture = await ContentLoadService.LoadContentAsync<Texture2D>(iconEntry.TextureFileName);

				if (iconTexture == null)
					throw new InvalidOperationException($"Failed to load Icon with Path: {iconEntry.TextureFileName}");

				//Due to async nature, we must ensure that it's still valid operation
				if (!applicationCollection.IsSlotActive(args.Slot) || args.SpellId != applicationCollection[args.Slot].Data.AuraSpellId)
					return;

				//This check exists to prevent only TARGET or other non-local player aura bars
				//from getting wrong icons after switching since this is ASYNC
				if (!IsHandlingTarget(args.Target))
					return;

				slot.AuraIconImage.SetSpriteTexture(iconTexture);
			});
		}
	}
}
