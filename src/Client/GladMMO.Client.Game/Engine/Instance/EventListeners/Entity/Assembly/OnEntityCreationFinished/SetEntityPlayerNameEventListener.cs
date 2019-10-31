using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SetEntityPlayerNameEventListener : PlayerCreationFinishedEventListener
	{
		private IReadonlyEntityGuidMappable<EntityGameObjectDirectory> ObjectDirectoryMappable { get; }

		private IEntityNameQueryable NameQueryable { get; }

		public SetEntityPlayerNameEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<EntityGameObjectDirectory> objectDirectoryMappable,
			[NotNull] IEntityNameQueryable nameQueryable) 
			: base(subscriptionService)
		{
			ObjectDirectoryMappable = objectDirectoryMappable ?? throw new ArgumentNullException(nameof(objectDirectoryMappable));
			NameQueryable = nameQueryable ?? throw new ArgumentNullException(nameof(nameQueryable));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			//If they have a floating name root we should try to set the name.
			EntityGameObjectDirectory directory = ObjectDirectoryMappable.RetrieveEntity(args.EntityGuid);
			GameObject nameRoot = directory.GetGameObject(EntityGameObjectDirectory.Type.NameRoot);

			IUIText text = nameRoot.GetComponent<IUIText>();

			if (text == null)
				return;

			if (NameQueryable.Exists(args.EntityGuid))
				text.Text = NameQueryable.Retrieve(args.EntityGuid);
			else
				UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
				{
					string name = await NameQueryable.RetrieveAsync(args.EntityGuid);

					//Maybe it got deleted before query was done.
					if (nameRoot == null)
						return;

					text.Text = NameQueryable.Retrieve(args.EntityGuid);
				});
		}
	}
}
