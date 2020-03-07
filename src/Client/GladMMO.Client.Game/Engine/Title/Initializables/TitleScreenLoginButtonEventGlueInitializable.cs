using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using GladMMO.Client;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ILoginButtonClickedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	public sealed class TitleScreenLoginButtonEventGlueInitializable : IGameInitializable, ILoginButtonClickedEventSubscribable
	{
		private IUIButton LoginButton { get; }

		/// <inheritdoc />
		public event EventHandler OnLoginButtonClicked;

		/// <inheritdoc />
		public TitleScreenLoginButtonEventGlueInitializable([KeyFilter(UnityUIRegisterationKey.Login)] [NotNull] IUIButton loginButton)
		{
			LoginButton = loginButton ?? throw new ArgumentNullException(nameof(loginButton));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			//When login button is pressed we should temporarily disable the interactivity of the login button.
			LoginButton.AddOnClickListener(() =>
			{
				LoginButton.IsInteractable = false;

				//We should just dispatch on click.
				OnLoginButtonClicked?.Invoke(this, EventArgs.Empty);
			});

			return Task.CompletedTask;
		}
	}
}
