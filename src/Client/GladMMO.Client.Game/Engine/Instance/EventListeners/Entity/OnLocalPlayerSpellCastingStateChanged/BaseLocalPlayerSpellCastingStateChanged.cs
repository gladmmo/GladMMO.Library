using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseLocalPlayerSpellCastingStateChanged : ThreadUnSafeBaseSingleEventListenerInitializable<ILocalPlayerSpellCastingStateChangedEventSubscribable, SpellCastingStateChangedEventArgs>
	{
		protected BaseLocalPlayerSpellCastingStateChanged(ILocalPlayerSpellCastingStateChangedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
