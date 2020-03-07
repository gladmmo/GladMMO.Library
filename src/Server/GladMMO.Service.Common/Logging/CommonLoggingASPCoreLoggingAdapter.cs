using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Common.Logging.Factory;
using Microsoft.Extensions.Logging;
using LogLevel = Common.Logging.LogLevel;

namespace GladMMO
{
	public sealed class CommonLoggingASPCoreLoggingAdapter : AbstractLogger
	{
		private ILogger<CommonLoggingASPCoreLoggingAdapter> Logger { get; }

		public CommonLoggingASPCoreLoggingAdapter([JetBrains.Annotations.NotNull] ILogger<CommonLoggingASPCoreLoggingAdapter> logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void WriteInternal(LogLevel level, object message, Exception exception)
		{
			switch (level)
			{
				case LogLevel.Trace:
					Logger.LogTrace(ComputeStringMessageValue(message));
					break;
				case LogLevel.Debug:
					Logger.LogDebug(ComputeStringMessageValue(message));
					break;
				case LogLevel.Info:
					Logger.LogInformation(ComputeStringMessageValue(message));
					break;
				case LogLevel.Warn:
					Logger.LogWarning(exception, ComputeStringMessageValue(message));
					break;
				case LogLevel.Error:
					Logger.LogError(ComputeStringMessageValue(message));
					break;
				case LogLevel.Fatal:
					Logger.LogCritical(exception, ComputeStringMessageValue(message));
					break;
				case LogLevel.Off:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(level), level, null);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string ComputeStringMessageValue(object message)
		{
			return message is string castedStringValue ? castedStringValue : message.ToString();
		}

		public override bool IsTraceEnabled => Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Trace);

		public override bool IsDebugEnabled => Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Debug);

		public override bool IsErrorEnabled => Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Error);

		public override bool IsFatalEnabled => Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Critical);

		public override bool IsInfoEnabled => Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Information);

		public override bool IsWarnEnabled => Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Warning);
	}
}
