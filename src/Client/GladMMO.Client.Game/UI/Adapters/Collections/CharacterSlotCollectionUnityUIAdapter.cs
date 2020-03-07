using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Exposed designers the ability to register collections of <see cref="IUICharacterSlot"/>s.
	/// </summary>
	public sealed class CharacterSlotCollectionUnityUIAdapter : BaseCollectionUnityUIAdapter<IUICharacterSlot>
	{
		[SerializeField]
		[Tooltip("The collection of character slots to aggregate.")]
		private CharacterSlotUIAdapter[] _elements;

		/// <inheritdoc />
		protected override IEnumerable<IUICharacterSlot> Elements => _elements;
	}
}
