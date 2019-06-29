using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	public class SharedInitializeCharacterControllerMappableEventListener : PlayerWorldRepresentationCreatedEventListener
	{
		private IEntityGuidMappable<CharacterController> CharacterControllerMappable { get; }

		public SharedInitializeCharacterControllerMappableEventListener(IEntityWorldRepresentationCreatedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<CharacterController> characterControllerMappable) 
			: base(subscriptionService)
		{
			CharacterControllerMappable = characterControllerMappable ?? throw new ArgumentNullException(nameof(characterControllerMappable));
		}

		protected override void OnPlayerWorldRepresentationCreated(EntityWorldRepresentationCreatedEventArgs args)
		{
			CharacterController characterController = args.EntityWorldRepresentation.GetComponent<CharacterController>();

			//TODO: Assert it being there
			CharacterControllerMappable.AddObject(args.EntityGuid, characterController);
		}
	}
}
