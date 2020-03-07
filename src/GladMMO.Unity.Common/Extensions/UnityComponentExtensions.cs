using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	public static class UnityComponentExtensions
	{
		//TODO: In C# 8 convert this to a prop
		/// <summary>
		/// Gets the root <see cref="GameObject"/> of the heirarchy that this component
		/// is attached to.
		/// </summary>
		/// <param name="component">The component.</param>
		/// <returns>The root game object.</returns>
		public static GameObject GetRootGameObject([NotNull] this Component component)
		{
			if(component == null) throw new ArgumentNullException(nameof(component));

			return component.gameObject.transform.root.gameObject;
		}
	}
}
