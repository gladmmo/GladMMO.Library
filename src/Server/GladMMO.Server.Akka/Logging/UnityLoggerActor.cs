using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Event;
using Common.Logging;

namespace GladMMO
{
	public sealed class UnityLoggerActor : ReceiveActor, ILogReceive
	{
		private ILog Logger { get; }

		public UnityLoggerActor(ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			Receive<Debug>(e => Logger.Debug(e.ToString()));
			Receive<Info>(e => Logger.Info(e.ToString()));
			Receive<Warning>(e => Logger.Warn(e.ToString()));
			Receive<Error>(e => Logger.Error(e.ToString()));
			Receive<InitializeLogger>(_ => Sender.Tell(new LoggerInitialized()));
		}
	}
}
