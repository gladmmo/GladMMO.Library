using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Common.Logging.Factory;

namespace GladMMO
{
	//From the Freecraft Proxy
	public sealed class ConsoleLogger : AbstractLogger
	{
		private LogLevel LoggingLevel { get; }

		/// <inheritdoc />
		public ConsoleLogger(LogLevel loggingLevel)
		{
			LoggingLevel = loggingLevel;
		}

		/// <inheritdoc />
		protected override void WriteInternal(LogLevel level, object message, Exception exception)
		{
			Console.WriteLine($"{level}: {message} {exception?.Message}");
		}

		/// <inheritdoc />
		public override bool IsTraceEnabled => LoggingLevel.HasFlag(LogLevel.Trace) || LoggingLevel == LogLevel.All;

		/// <inheritdoc />
		public override bool IsDebugEnabled => LoggingLevel.HasFlag(LogLevel.Debug) || LoggingLevel == LogLevel.All;

		/// <inheritdoc />
		public override bool IsErrorEnabled => LoggingLevel.HasFlag(LogLevel.Error) || LoggingLevel == LogLevel.All;

		/// <inheritdoc />
		public override bool IsFatalEnabled => LoggingLevel.HasFlag(LogLevel.Fatal) || LoggingLevel == LogLevel.All;

		/// <inheritdoc />
		public override bool IsInfoEnabled => LoggingLevel.HasFlag(LogLevel.Info) || LoggingLevel == LogLevel.All;

		/// <inheritdoc />
		public override bool IsWarnEnabled => LoggingLevel.HasFlag(LogLevel.Warn) || LoggingLevel == LogLevel.All;
	}
}