﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	public sealed class ServiceEndpointKey
	{
		/// <summary>
		/// Identifier of the service endpoint entry.
		/// </summary>
		public string ServiceName { get; internal set; }

		/// <summary>
		/// The locale of the service.
		/// </summary>
		public ClientRegionLocale Locale { get; internal set; }

		/// <summary>
		/// Indicates the deployment/enviroment mode.
		/// </summary>
		public DeploymentMode Mode { get; internal set; }

		public ServiceEndpointKey([JetBrains.Annotations.NotNull] string serviceName, ClientRegionLocale locale, DeploymentMode mode)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), locale)) throw new InvalidEnumArgumentException(nameof(locale), (int) locale, typeof(ClientRegionLocale));
			if (!Enum.IsDefined(typeof(DeploymentMode), mode)) throw new InvalidEnumArgumentException(nameof(mode), (int) mode, typeof(DeploymentMode));
			if (string.IsNullOrWhiteSpace(serviceName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceName));

			ServiceName = serviceName.ToUpper();
			Locale = locale;
			Mode = mode;
		}
	}

	[Table("service_endpoints")]
	public class ServiceEndpointModel
	{
		/// <summary>
		/// Identifier of the service endpoint entry.
		/// </summary>
		public int ServiceId { get; internal set; }

		/// <summary>
		/// The foreign relation service reference.
		/// See: <see cref="ServiceEntryModel"/>
		/// </summary>
		[ForeignKey(nameof(ServiceId))]
		public virtual ServiceEntryModel Service { get; internal set; }

		/// <summary>
		/// The locale of the service.
		/// </summary>
		public ClientRegionLocale Locale { get; internal set; }

		/// <summary>
		/// Indicates the deployment/enviroment mode.
		/// </summary>
		public DeploymentMode Mode { get; internal set; }

		/// <summary>
		/// The endpoint address for the service.
		/// </summary>
		public ResolvedEndpoint Endpoint { get; internal set; }

		public ServiceEndpointModel(int serviceId,
			ClientRegionLocale locale, 
			DeploymentMode mode,
			[JetBrains.Annotations.NotNull] ResolvedEndpoint endpoint)
		{
			if (!Enum.IsDefined(typeof(DeploymentMode), mode)) throw new InvalidEnumArgumentException(nameof(mode), (int) mode, typeof(DeploymentMode));
			if (!Enum.IsDefined(typeof(ClientRegionLocale), locale)) throw new InvalidEnumArgumentException(nameof(locale), (int) locale, typeof(ClientRegionLocale));
			if (serviceId <= 0) throw new ArgumentOutOfRangeException(nameof(serviceId));

			ServiceId = serviceId;
			Locale = locale;
			Mode = mode;
			Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		internal ServiceEndpointModel()
		{
			
		}
	}
}