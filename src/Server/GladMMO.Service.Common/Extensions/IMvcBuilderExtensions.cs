using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GladMMO
{
	public static class IMvcBuilderExtensions
	{
		/// <summary>
		/// Registers the general <see cref="HealthCheckController"/> with the MVC
		/// controllers. See controller documentation for what it does and how it works.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IMvcBuilder RegisterHealthCheckController(this IMvcBuilder builder)
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));

			//See AddControllersAsServices: https://github.com/aspnet/Mvc/blob/747420e5aa7cc2c7834cfb9731510286ded6fc03/src/Microsoft.AspNetCore.Mvc.Core/DependencyInjection/MvcCoreMvcCoreBuilderExtensions.cs#L107
			builder.ConfigureApplicationPartManager(manager => manager.FeatureProviders.Add(new GenericControllerFeatureProvider()));

			return builder;
		}

		/// <summary>
		/// ASP Core feature provider that registers a health check controller.
		/// </summary>
		private class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
		{
			/// <inheritdoc />
			public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
			{
				//We need to check this because, for some reason, this could be called twice? Was registered twice better for some reason
				//So don't remove this check
				if(!feature.Controllers.Contains(typeof(HealthCheckController).GetTypeInfo()))
					feature.Controllers.Add(typeof(HealthCheckController).GetTypeInfo());

				//VersionController
				if(!feature.Controllers.Contains(typeof(VersionController).GetTypeInfo()))
					feature.Controllers.Add(typeof(VersionController).GetTypeInfo());
			}
		}
	}
}
