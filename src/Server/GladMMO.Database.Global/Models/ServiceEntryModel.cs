using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("services")]
	public class ServiceEntryModel
	{
		/// <summary>
		/// Unique identifier for the server.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Id")]
		public int ServiceId { get; internal set; }

		/// <summary>
		/// Represents the name of the service.
		/// </summary>
		public string ServiceName { get; internal set; }

		public ServiceEntryModel(int serviceId, string serviceName)
		{
			if (string.IsNullOrEmpty(serviceName)) throw new ArgumentException("Value cannot be null or empty.", nameof(serviceName));

			ServiceId = serviceId;
			ServiceName = serviceName.ToUpper();
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		internal ServiceEntryModel()
		{
			
		}
	}
}
