using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Akka.Event;
using Common.Logging;

namespace GladMMO
{
	public sealed class UnityAkkaActorLoggerAdapter : LoggingAdapterBase
	{
		private ILog Logger { get; }

		public UnityAkkaActorLoggerAdapter(ILog logger) 
			: base(new DefaultLogMessageFormatter())
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void NotifyError(object message)
		{
			Logger.Error(message);
		}

		protected override void NotifyError(Exception cause, object message)
		{
			Logger.Error(message, cause);
		}

		protected override void NotifyWarning(object message)
		{
			Logger.Warn(message);
		}

		protected override void NotifyWarning(Exception cause, object message)
		{
			Logger.Warn(message, cause);
		}

		protected override void NotifyInfo(object message)
		{
			Logger.Info(message);
		}

		protected override void NotifyInfo(Exception cause, object message)
		{
			Logger.Info(message, cause);
		}

		protected override void NotifyDebug(object message)
		{
			Logger.Debug(message);
		}

		protected override void NotifyDebug(Exception cause, object message)
		{
			Logger.Debug(message, cause);
		}

		public override bool IsDebugEnabled => Logger.IsDebugEnabled;

		public override bool IsInfoEnabled => Logger.IsInfoEnabled;

		public override bool IsWarningEnabled => Logger.IsWarnEnabled;

		public override bool IsErrorEnabled => Logger.IsErrorEnabled;
	}
}
