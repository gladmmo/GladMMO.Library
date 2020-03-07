using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	//This is the button event for when they hit the "Add" button on the Add Friend popup modal.
	[AdditionalRegisterationAs(typeof(IAddFriendModalClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ModalAddFriendButtonClickedGlueStartable : BaseButtonClickGlueStartable, IAddFriendModalClickedEventSubscribable
	{
		public const UnityUIRegisterationKey ButtonKey = UnityUIRegisterationKey.AddFriendModalWindow;

		public ModalAddFriendButtonClickedGlueStartable([KeyFilter(ButtonKey)] [NotNull] IUIButton createButton)
			: base(createButton)
		{

		}
	}
}
