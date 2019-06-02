using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	/*public sealed class DefaultObjectUpdateBlockDispatcher : IObjectUpdateBlockDispatcher
	{
		private IObjectUpdateBlockHandler[] Handlers { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public DefaultObjectUpdateBlockDispatcher([NotNull] IEnumerable<IObjectUpdateBlockHandler> handlers, [NotNull] ILog logger)
		{
			if(handlers == null) throw new ArgumentNullException(nameof(handlers));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			//Order by update type.
			//Normally I'd use some fancy data structure but we need to care about perf when it comes to this packet.
			Handlers = handlers
				.OrderBy(h => (int)h.UpdateType)
				.ToArray();
		}

		/// <inheritdoc />
		public void Dispatch(ObjectUpdateBlock updateBlock)
		{
			//TODO: Don't supress error.
			if(Handlers.Length <= (int)updateBlock.UpdateType)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Encountered unhandable {nameof(ObjectUpdateBlock)} with Type: {updateBlock.UpdateType}.");

				//throw new InvalidOperationException($"Cannot handle Type: {updateBlock} because it has UpdateType: {updateBlock.UpdateType} which is outside the bounds of handling in {GetType().Name}.");
			}
			else
				Handlers[(int)updateBlock.UpdateType].HandleUpdateBlock(updateBlock);
		}
	}*/
}
