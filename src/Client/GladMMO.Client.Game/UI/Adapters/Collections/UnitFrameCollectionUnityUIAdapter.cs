using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Exposed designers the ability to register collections of <see cref="IUILabeledButton"/>s.
	/// </summary>
	public sealed class UnitFrameCollectionUnityUIAdapter : BaseCollectionUnityUIAdapter<IUIUnitFrame>
	{
		[SerializeField]
		[Tooltip("The collection of labeled buttons to aggregate.")]
		private UnitFrameElementsAdapter[] _elements;

		/// <inheritdoc />
		protected override IEnumerable<IUIUnitFrame> Elements => _elements;
	}
}
