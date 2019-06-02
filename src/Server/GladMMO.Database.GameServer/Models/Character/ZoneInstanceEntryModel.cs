using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("zone_endpoints")]
	public class ZoneInstanceEntryModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ZoneId { get; private set; }

		/// <summary>
		/// The address (IP or even domain) of the instance/zone.
		/// </summary>
		[Required]
		public string ZoneServerAddress { get; private set; }

		/// <summary>
		/// The public port the zone server can be connected to.
		/// </summary>
		[Required]
		public short ZoneServerPort { get; private set; }

		/// <summary>
		/// The ID of the world this zone instance is running/based on.
		/// There can be many instances running the same world. It's ok
		/// that this isn't unique.
		/// </summary>
		[Required]
		[Range(0, long.MaxValue)]
		public long WorldId { get; private set; }

		/// <summary>
		/// Navigation property for the world entry this zone instance
		/// is running.
		/// </summary>
		[ForeignKey(nameof(WorldId))]
		public virtual WorldEntryModel WorldEntry { get; private set; }

		/// <inheritdoc />
		public ZoneInstanceEntryModel(string zoneServerAddress, short zoneServerPort, long worldId)
		{
			if(string.IsNullOrWhiteSpace(zoneServerAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(zoneServerAddress));
			if(zoneServerPort < 0) throw new ArgumentOutOfRangeException(nameof(zoneServerPort));
			if(worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			ZoneServerAddress = zoneServerAddress;
			ZoneServerPort = zoneServerPort;
			WorldId = worldId;
		}

		private ZoneInstanceEntryModel()
		{
			
		}
	}
}
