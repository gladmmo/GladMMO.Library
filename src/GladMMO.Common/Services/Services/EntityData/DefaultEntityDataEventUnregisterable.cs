using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultEntityDataEventUnregisterable : IEntityDataEventUnregisterable
	{
		private Action RemoveDelegate { get; }

		private bool isUnregistered { get; set; }

		/// <inheritdoc />
		public DefaultEntityDataEventUnregisterable([NotNull] Action removeDelegate)
		{
			RemoveDelegate = removeDelegate ?? throw new ArgumentNullException(nameof(removeDelegate));
			isUnregistered = false;
		}

		/// <inheritdoc />
		public void Unregister()
		{
			if(!isUnregistered)
			{
				RemoveDelegate();
				isUnregistered = true;
			}
		}
	}
}
