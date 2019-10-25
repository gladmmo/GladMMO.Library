using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IManualAddFriendClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	public sealed class ManualAddFriendButtonClickedGlueStartable : BaseButtonClickGlueStartable, IManualAddFriendClickedEventSubscribable
	{
		public const UnityUIRegisterationKey ButtonKey = UnityUIRegisterationKey.AddFriendsButton;

		public ManualAddFriendButtonClickedGlueStartable([KeyFilter(ButtonKey)] [NotNull] IUIButton createButton)
			: base(createButton)
		{

		}
	}
}
