using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface ILocalPlayerSpellCastingStateChangedEventPublisher : IEventPublisher<ILocalPlayerSpellCastingStateChangedEventSubscribable, SpellCastingStateChangedEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(ILocalPlayerSpellCastingStateChangedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(ILocalPlayerSpellCastingStateChangedEventPublisher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerSpellCastingStateChangedEventPublisher : ILocalPlayerSpellCastingStateChangedEventPublisher, ILocalPlayerSpellCastingStateChangedEventSubscribable, IGameInitializable
	{
		public event EventHandler<SpellCastingStateChangedEventArgs> OnSpellCastingStateChanged;

		public void PublishEvent(object sender, SpellCastingStateChangedEventArgs eventArgs)
		{
			OnSpellCastingStateChanged?.Invoke(sender, eventArgs);
		}

		//Get into the scene.
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
