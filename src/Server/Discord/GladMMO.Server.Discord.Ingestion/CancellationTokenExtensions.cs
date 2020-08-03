using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace GladMMO
{
	//From: https://github.com/dotnet/runtime/issues/14991
	public static class CancellationTokenExtensions
	{
		public static CancellationTokenAwaiter GetAwaiter(this CancellationToken cancellationToken)
		{
			return new CancellationTokenAwaiter(cancellationToken);
		}

		public struct CancellationTokenAwaiter : INotifyCompletion, IDisposable
		{
			private CancellationToken CancellationToken { get; }

			private List<IDisposable> Disposables { get; }

			public CancellationTokenAwaiter(CancellationToken cancellationToken)
			{
				this.CancellationToken = cancellationToken;
				Disposables = new List<IDisposable>();
			}

			public bool IsCompleted => CancellationToken.IsCancellationRequested;

			public void OnCompleted(Action continuation)
			{
				CancellationTokenRegistration registration = CancellationToken.Register(continuation);
				Disposables.Add(registration);
			}

			// No any wait required.
			public void GetResult()
			{

			}

			public void Dispose()
			{
				foreach(var d in Disposables)
					d.Dispose();
			}
		}
	}
}
