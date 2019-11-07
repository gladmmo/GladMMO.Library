using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseLocalPlayerSpellCastingStateChanged : BaseSingleEventListenerInitializable<ILocalPlayerSpellCastingStateChangedEventSubscribable, SpellCastingStateChangedEventArgs>
	{
		protected BaseLocalPlayerSpellCastingStateChanged(ILocalPlayerSpellCastingStateChangedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
