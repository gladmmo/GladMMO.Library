using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class CharacterSelectionUpdateCharacterSelectedNameOnCharacterSelectionEventListener : BaseSingleEventListenerInitializable<ICharacterSelectionButtonClickedEventSubscribable, CharacterButtonClickedEventArgs>
	{
		private IUIText SelectedCharacterText { get; }

		private IEntityNameQueryable NameryQueryable { get; } 

		/// <inheritdoc />
		public CharacterSelectionUpdateCharacterSelectedNameOnCharacterSelectionEventListener(ICharacterSelectionButtonClickedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.CharacterSelection)] [NotNull] IUIText selectedCharacterText,
			[NotNull] IEntityNameQueryable nameryQueryable)
			: base(subscriptionService)
		{
			SelectedCharacterText = selectedCharacterText ?? throw new ArgumentNullException(nameof(selectedCharacterText));
			NameryQueryable = nameryQueryable ?? throw new ArgumentNullException(nameof(nameryQueryable));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, CharacterButtonClickedEventArgs args)
		{
			NameryQueryable.EnsureExists(args.CharacterGuid);
			SelectedCharacterText.Text = NameryQueryable.Retrieve(args.CharacterGuid);
		}
	}
}
