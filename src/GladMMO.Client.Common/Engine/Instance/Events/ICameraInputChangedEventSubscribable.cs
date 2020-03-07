using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ICameraInputChangedEventSubscribable
	{
		event EventHandler<CameraInputChangedEventArgs> OnCameraInputChange;
	}

	public sealed class CameraInputChangedEventArgs : EventArgs, IEquatable<CameraInputChangedEventArgs>
	{
		public float Rotation { get; }

		public CameraInputChangedEventArgs(float rotation)
		{
			Rotation = rotation;
		}

		public static bool operator ==(CameraInputChangedEventArgs obj1, CameraInputChangedEventArgs obj2)
		{
			if(ReferenceEquals(obj1, obj2))
				return true;

			if(ReferenceEquals(obj1, null))
				return false;

			if(ReferenceEquals(obj2, null))
				return false;

			return obj1.Equals(obj2);
		}

		public static bool operator !=(CameraInputChangedEventArgs obj1, CameraInputChangedEventArgs obj2)
		{
			return !(obj1 == obj2);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;

			if(obj is CameraInputChangedEventArgs second)
				return this.Equals(second);

			return false;
		}

		public bool Equals([NotNull] CameraInputChangedEventArgs other)
		{
			if(other == null)
				return false;

			return Math.Abs(other.Rotation - Rotation) < 0.005f;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return Rotation.GetHashCode();
		}
	}
}