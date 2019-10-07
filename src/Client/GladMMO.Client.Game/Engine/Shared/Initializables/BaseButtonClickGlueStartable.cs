using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseButtonClickGlueStartable : IGameStartable, IButtonClickedEventSubscribable
	{
		public event EventHandler<ButtonClickedEventArgs> OnButtonClicked;

		private IUIButton ReferenceButton { get; }

		public BaseButtonClickGlueStartable([NotNull] IUIButton referenceButton)
		{
			ReferenceButton = referenceButton ?? throw new ArgumentNullException(nameof(referenceButton), "Maybe you're missing autofac key filter.");
		}

		public async Task OnGameStart()
		{
			ReferenceButton.AddOnClickListener(() => { OnButtonClicked?.Invoke(ReferenceButton, new ButtonClickedEventArgs(ReferenceButton)); });
		}
	}
}
