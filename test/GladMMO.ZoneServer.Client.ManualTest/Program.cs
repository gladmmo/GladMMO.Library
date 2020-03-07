using System; using FreecraftCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using ProtoBuf;
using ProtoBuf.Meta;

namespace GladMMO
{
	class Program
	{
		static async Task Main(string[] args)
		{
			ProtobufNetGladNetSerializerAdapter serializer = new ProtobufNetGladNetSerializerAdapter();

			Unity3DProtobufPayloadRegister payloadRegister = new Unity3DProtobufPayloadRegister();
			payloadRegister.RegisterDefaults();
			payloadRegister.Register();

			var client = new GladMMOUnmanagedNetworkClient<DotNetTcpClientNetworkClient, GameServerPacketPayload,
					GameClientPacketPayload, IGamePacketPayload>(new DotNetTcpClientNetworkClient(), serializer, new ConsoleLogger(LogLevel.All))
				.AsManaged();

			await client.ConnectAsync(IPAddress.Parse("72.190.177.214"), 5006);
			Thread.Sleep(3000);

			Console.WriteLine("Enter Character ID for test:");

			int characterId = int.Parse(Console.ReadLine());

			await client.SendMessage(new ClientSessionClaimRequestPayload("Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjYyN0YyQUFDMTZERTlENjNDMkY3NDQyQzk1OUFBNjEyQjIyOTlENDciLCJ0eXAiOiJKV1QifQ.eyJzdWIiOiIyNyIsIm5hbWUiOiJUZXN0UGxheWZhYiIsInBmaWQiOiJEMTdENUI1RDY5RTY3QTM2IiwidG9rZW5fdXNhZ2UiOiJhY2Nlc3NfdG9rZW4iLCJqdGkiOiIyMDM2Zjg1MC04ODY5LTQ4YzUtOWRkOS0xYWI4MzIzZjBkOTkiLCJzY29wZSI6Im9wZW5pZCIsImF1ZCI6ImF1dGgtc2VydmVyIiwiYXpwIjoiVlJHdWFyZGlhbnNBdXRoZW50aWNhdGlvbiIsIm5iZiI6MTU1OTYzNzkzMywiZXhwIjoxNTU5NjQxNTMzLCJpYXQiOjE1NTk2Mzc5MzMsImlzcyI6Imh0dHBzOi8vYXV0aC52cmd1YXJkaWFucy5uZXQvIn0.oWlUhM3JMI1jC6Irh8lUeTmzcIIl2TJfgps0dHSshS6-TcJsL37u3xP_xXE2x0FH3HkXbAPyCL2hYr7KdSnCATh1G1Ar9vqt3XY0HWw7lOCovmAYjPHPgaHZXGnlk6P35A4bpCK-gLPi4xTW6rHYa1SJt_oeNQfFuElaUTR1cj-TajqHCV6P3xb3cEcsAD4Z4Pye0x4RUANN6NKpMvc5c_GPfnoRDpPFQwM7e8616vPBoN2vR7k9pnvsaJPBdnlZrMA8fVmvigO4ARoyGMJNkgsVonF7Oyr1LfLJu47tuqEqzKL9pRkOFYvUyCHniImut6o0JMX5t2LWwA-GOhiRmzCLuiVV0GMHQLFRkZ7h3lw4lXC6-nbq-k9YQKbj3jDM8uLyQv0VdxJjfF28DIAs4Eo_5A5r6R48p0-opiU0SVLPK-IfNWCzl_jFXCpOq0otoKVx-lyl89eyMYATny4tr1qHT2FG6Y8Pdm8skz13iZltAivy-I6W5RbeZ7COgwtaHmcwqTOY7kpFAWZ0rylFTDbKL7K-Kf7QMNQfUw3yP8KOWk4KL6Wa9EFFkBzwcxFKX-fdVg08xTBYZWj5GuCU_gWCTYBviiBbwCC4H2yQcJ9gLvW6tVRDCPfbpZiuMU8FrUk_JflQoy-QXUV1L-CZofra-uUGmHmLya5gULbR6qI", characterId));

			int packetCount = 0;

			try
			{
				while(true)
				{
					NetworkIncomingMessage<GameServerPacketPayload> message = await client.ReadMessageAsync()
						.ConfigureAwaitFalse();

					Console.WriteLine($"\n#{packetCount} - Recieved Message Type: {message.Payload.GetType().Name}");
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
