using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GladMMO
{
	/// <summary>
	/// ASP Core Middleware for validating Event Grid webhook registerations.
	/// </summary>
	public class EventGridValidationMiddleware
	{
		private RequestDelegate Next { get; }

		public EventGridValidationMiddleware([NotNull] RequestDelegate next)
		{
			Next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public async Task InvokeAsync(HttpContext context)
		{
			//Event grid Discord Commands go to this path
			//api/discord/command
			if (!context.Request.Path.HasValue || !context.Request.Path.Value.Contains("api/discord/command"))
			{
				// Call the next delegate/middleware in the pipeline
				await Next(context);
				return;
			}

			//Without buffering then resetting stream won't work.
			context.Request.EnableBuffering(sizeof(byte) * 1024 * 1000);

			//TODO: Use HttpRequestStreamReader. Had to switch until .NET 5 due to: https://github.com/dotnet/aspnetcore/issues/13834
			//It's a Discord command
			using(var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, -1, true)) //don't own it, otherwise this will DISPOSE!!
			{
				string json = await reader.ReadToEndAsync();
				EventGridEvent[] events = JsonConvert.DeserializeObject<EventGridEvent[]>(json);

				if (!events.Any())
					return;

				//TODO: This is INSECURE AND UNSAFE. Someone else could end up registering a webhook from Azure if we don't protect eventually
				//Check for validation event
				if (events[0].EventType == EventTypes.EventGridSubscriptionValidationEvent)
					context.Request.Path = $"{context.Request.Path}/{GladMMOServiceDiscordConstants.EVENT_GRID_VALIDATE_ACTION}";
				else
				{
					//We're a DISCORD BOT COMMAND!
					//TODO: We're assuming we only have 1 and we're not batch events. By default batch events are turned off on Event Grid.
					string innerCommandEventString = (string) events[0].Data;
				}
			}

			//Reset stream before forwarding anything, otherwise it'll look wrong!!
			context.Request.Body.Position = 0;

			// Call the next delegate/middleware in the pipeline
			await Next(context);
		}
	}
}
