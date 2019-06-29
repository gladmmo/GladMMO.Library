using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnEntityWorldRepresentationCreatedInitializeGameObjectDirectoryEventListener : PlayerWorldRepresentationCreatedEventListener
	{
		private IEntityGuidMappable<EntityGameObjectDirectory> GameObjectDirectoryMappable { get; }

		public OnEntityWorldRepresentationCreatedInitializeGameObjectDirectoryEventListener(IEntityWorldRepresentationCreatedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<EntityGameObjectDirectory> gameObjectDirectoryMappable)
			: base(subscriptionService)
		{
			GameObjectDirectoryMappable = gameObjectDirectoryMappable ?? throw new ArgumentNullException(nameof(gameObjectDirectoryMappable));
		}

		protected override void OnPlayerWorldRepresentationCreated(EntityWorldRepresentationCreatedEventArgs args)
		{
			EntityGameObjectDirectory characterController = args.EntityWorldRepresentation.GetComponent<EntityGameObjectDirectory>();

			//TODO: Assert it being there
			GameObjectDirectoryMappable.AddObject(args.EntityGuid, characterController);
		}
	}
}
