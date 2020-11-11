using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GladMMO
{
	/// <summary>
	/// ASP Core Middleware for Event Grid event routing.
	/// </summary>
	public class EventGridEventMiddleware
	{
		private RequestDelegate Next { get; }

		public EventGridEventMiddleware([NotNull] RequestDelegate next)
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

			//
		}
	}
}
