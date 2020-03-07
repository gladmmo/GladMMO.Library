﻿using System; using FreecraftCore;
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
		/// <summary>
		/// The zone instance expiration time.
		/// Checkins must happen before this 10 minute mark.
		/// </summary>
		public static long ExpirationTimeLength = TimeSpan.FromMinutes(10).Ticks;

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
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

		/// <summary>
		/// The UTC tick time of the initial zone registration.
		/// </summary>
		[Required]
		public long RegistrationTime { get; private set; }

		/// <summary>
		/// The UTC tick time of the last zoneserver checkin time.
		/// </summary>
		[Required]
		public long LastCheckinTime { get; private set; }

		/// <summary>
		/// Indicates if the registration has expired due to lack of checkin.
		/// </summary>
		public bool isExpired => (DateTime.UtcNow.Ticks - LastCheckinTime) >= ExpirationTimeLength;

		/// <inheritdoc />
		public ZoneInstanceEntryModel(int zoneId, string zoneServerAddress, short zoneServerPort, long worldId)
		{
			if(string.IsNullOrWhiteSpace(zoneServerAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(zoneServerAddress));
			if(zoneServerPort < 0) throw new ArgumentOutOfRangeException(nameof(zoneServerPort));
			if(worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));
			if (zoneId <= 0) throw new ArgumentOutOfRangeException(nameof(zoneId));

			ZoneId = zoneId;
			ZoneServerAddress = zoneServerAddress;
			ZoneServerPort = zoneServerPort;
			WorldId = worldId;

			RegistrationTime = DateTime.UtcNow.Ticks;
			LastCheckinTime = RegistrationTime;
		}

		/// <summary>
		/// Updates the <see cref="LastCheckinTime"/> to the current time.
		/// </summary>
		public void UpdateCheckinTime()
		{
			LastCheckinTime = DateTime.UtcNow.Ticks;
		}

		private ZoneInstanceEntryModel()
		{
			
		}
	}
}
