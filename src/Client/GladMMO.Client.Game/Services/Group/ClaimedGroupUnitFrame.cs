using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class ClaimedGroupUnitFrame
	{
		public IUIUnitFrame UnitFrame { get; }

		private List<IEntityDataEventUnregisterable> Unregisterables { get; }

		/// <inheritdoc />
		public ClaimedGroupUnitFrame(IUIUnitFrame unitFrame)
		{
			UnitFrame = unitFrame;
			Unregisterables = new List<IEntityDataEventUnregisterable>(5);
		}

		public void RegisterUnregisterableCallback([NotNull] IEntityDataEventUnregisterable unregisterable)
		{
			if(unregisterable == null) throw new ArgumentNullException(nameof(unregisterable));
			Unregisterables.Add(unregisterable);
		}

		public void ClearRegisteredCallbacks()
		{
			foreach(var unr in Unregisterables)
				unr.Unregister();

			Unregisterables.Clear();
		}
	}
}
