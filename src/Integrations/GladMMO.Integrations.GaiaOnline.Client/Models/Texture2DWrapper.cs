using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO.GaiaOnline
{
	/// <summary>
	/// Used in place of a Texture2D. Allowing the Texture2D initialization to
	/// occur on the main Unity3D where it has to. Otherwise Unity3D will throw.
	/// </summary>
	public sealed class Texture2DWrapper
	{
		/// <summary>
		/// Privately managed byte representation.
		/// </summary>
		private byte[] ImageBytes { get; set; } //the setter is only for clearing after initialization. Should help reduce RAM usage for large images.

		/// <summary>
		/// Lazily initialized <see cref="Texture2D"/> from the internally managed
		/// <see cref="ImageBytes"/>. This HAS to be lazy loaded in some fashion due to Unity3D constraints for Unity API use on
		/// the non-main game thread. Lazy semantics are public exposed as a warning to consumers.
		/// </summary>
		public Lazy<Texture2D> Texture { get; }

		//TODO: Can we make this protected? Or will the XML deserializer breakdown?
		public Texture2DWrapper()
		{
			//Slight chance the Texture2D could be referenced on another thread. Let the consumer deal with Unity thread safety
			Texture = new Lazy<Texture2D>(CreateTextureFromContainedBytes, true);
		}

		private Texture2D CreateTextureFromContainedBytes()
		{
			if(ImageBytes == null)
				throw new InvalidOperationException($"Cannot create a {nameof(Texture2D)} from a null byte chunk.");

			Texture2D tex = new Texture2D(2, 2);

			tex.LoadImage(ImageBytes);

			//Nulled for RAM purposes. We want this to get GC'd. Imagine having to double RAM usage textures, would be bad.
			ImageBytes = null;

			return tex;
		}

		/// <summary>
		/// Creates a new wrapper for a lazily initialized <see cref="Texture2D"/>
		/// which is constructed from the provided <see cref="ImageBytes"/>.
		/// </summary>
		/// <param name="imageBytes">The bytes.</param>
		public Texture2DWrapper(byte[] imageBytes)
			: this() //also need to call constructorless for lazy init 
		{
			//We should accept empty arrays for empty pictures I suppose? Maybe?
			if(imageBytes == null) throw new ArgumentNullException(nameof(imageBytes));

			ImageBytes = imageBytes;
		}
	}
}
