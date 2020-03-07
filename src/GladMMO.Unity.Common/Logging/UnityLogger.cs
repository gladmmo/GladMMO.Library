using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Common.Logging;
using Common.Logging.Factory;
using UnityEngine;

namespace GladMMO
{
	public sealed class UnityLogger : AbstractLogger
	{
		public LogLevel LoggingLevel { get; }

		/// <inheritdoc />
		public UnityLogger(LogLevel loggingLevel)
		{
			LoggingLevel = loggingLevel;
		}

		/// <inheritdoc />
		protected override void WriteInternal(LogLevel level, object message, Exception exception)
		{
			//TODO: Add exception logging
			switch(level)
			{
				case LogLevel.Trace:
				case LogLevel.Debug:
				case LogLevel.Info:
					UnityEngine.Debug.Log(message);
					break;
				case LogLevel.Warn:
					UnityEngine.Debug.LogWarning(message);
					break;
				case LogLevel.Error:
				case LogLevel.Fatal:
					UnityEngine.Debug.LogError(message);
					break;
				case LogLevel.Off:
					break;
			}
		}

		/// <inheritdoc />
		public override bool IsTraceEnabled => CheckIfLoggingStateEnabled(LogLevel.Trace);

		/// <inheritdoc />
		public override bool IsDebugEnabled => CheckIfLoggingStateEnabled(LogLevel.Debug);

		/// <inheritdoc />
		public override bool IsErrorEnabled => CheckIfLoggingStateEnabled(LogLevel.Error);

		/// <inheritdoc />
		public override bool IsFatalEnabled => CheckIfLoggingStateEnabled(LogLevel.Fatal);

		/// <inheritdoc />
		public override bool IsInfoEnabled => CheckIfLoggingStateEnabled(LogLevel.Info);

		/// <inheritdoc />
		public override bool IsWarnEnabled => CheckIfLoggingStateEnabled(LogLevel.Warn);

		/// <inheritdoc />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool CheckIfLoggingStateEnabled(LogLevel level)
		{
			return LoggingLevel == LogLevel.All || (LoggingLevel & level) != 0;
		}
	}
}