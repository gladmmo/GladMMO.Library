using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.SignalR.Client;

namespace GladMMO
{
	public static class SignalRClientExtensions
	{
		public static void RegisterClientInterface<TInterfaceType>([NotNull] this HubConnection connection, [NotNull] TInterfaceType handlerInstance)
		{
			if(connection == null) throw new ArgumentNullException(nameof(connection));
			if(handlerInstance == null) throw new ArgumentNullException(nameof(handlerInstance));

			foreach(MethodInfo mi in typeof(TInterfaceType).GetMethods())
			{
				if(mi.ReturnType != typeof(Task))
					throw new InvalidOperationException($"Encountered Method on {typeof(TInterfaceType).Name} with non-{nameof(Task)} return type.");

				connection.On(mi.Name, mi.GetParameters().Select(p => p.ParameterType).ToArray(),
					objects => (Task)mi.Invoke(handlerInstance, objects));
			}
		}
	}
}