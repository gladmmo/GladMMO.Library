using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class LocalPlayerTargetChangedEventListener : BaseSingleEventListenerInitializable<ILocalPlayerTargetChangedEventListener, LocalPlayerTargetChangedEventArgs>
	{
		protected IUIUnitFrame TargetUnitFrame { get; }

		protected ILog Logger { get; }

		protected LocalPlayerTargetChangedEventListener(ILocalPlayerTargetChangedEventListener subscriptionService,
			[NotNull] ILog logger,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIUnitFrame targetUnitFrame) 
			: base(subscriptionService)
		{
			TargetUnitFrame = targetUnitFrame ?? throw new ArgumentNullException(nameof(targetUnitFrame));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, LocalPlayerTargetChangedEventArgs args)
		{
			//Just forward so consumers AKA 3rd party users can have a cleaner API to implement.
			OnLocalPlayerTargetChanged(args);
		}

		protected abstract void OnLocalPlayerTargetChanged(LocalPlayerTargetChangedEventArgs args);
	}
}
