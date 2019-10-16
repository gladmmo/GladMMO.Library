using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeTargetNameEventListener : LocalPlayerTargetChangedEventListener
	{
		private IEntityNameQueryable NameQueryable { get; }

		public InitializeTargetNameEventListener(ILocalPlayerTargetChangedEventListener subscriptionService,
			ILog logger,
			[KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIUnitFrame targetUnitFrame,
			[NotNull] IEntityNameQueryable nameQueryable) 
			: base(subscriptionService, logger, targetUnitFrame)
		{
			NameQueryable = nameQueryable ?? throw new ArgumentNullException(nameof(nameQueryable));
		}

		protected override void OnLocalPlayerTargetChanged(LocalPlayerTargetChangedEventArgs args)
		{
			//If it's exists, it's the best case scenario and we're good to go.
			if (NameQueryable.Exists(args.TargetedEntity))
			{
				string name = NameQueryable.Retrieve(args.TargetedEntity);
				TargetUnitFrame.UnitName.Text = name;
			}
			else
			{
				//TODO: We need to handle this case better.
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Could not load target entity name.");
			}
		}
	}
}
