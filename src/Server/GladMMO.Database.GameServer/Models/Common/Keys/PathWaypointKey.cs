using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	//TODO: If owned models are ever available we should do this.
	[Owned]
	public sealed class PathWaypointKey : IComparable, IComparable<PathWaypointKey>, IEquatable<PathWaypointKey>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int PathId { get; private set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int PointId { get; private set; }

		/// <inheritdoc />
		public PathWaypointKey(int pathId, int pointId)
		{
			if(pathId <= 0) throw new ArgumentOutOfRangeException(nameof(pathId));
			if(pointId <= 0) throw new ArgumentOutOfRangeException(nameof(pointId));

			PathId = pathId;
			PointId = pointId;
		}

		/// <inheritdoc />
		public int CompareTo(object obj)
		{
			if(obj is PathWaypointKey objCasted)
				return ((IComparable<PathWaypointKey>)this).CompareTo(objCasted);

			throw new ArgumentException($"{nameof(obj)} is not {nameof(PathWaypointKey)}");
		}

		/// <inheritdoc />
		public int CompareTo(PathWaypointKey other)
		{
			if(other == null) throw new ArgumentNullException(nameof(other));

			if(other == this)
				return 0;

			if(other.PathId == this.PathId)
				return 0;

			if(this.PathId > other.PathId)
				return 1;
			else
				return -1;
		}

		/// <inheritdoc />
		public bool Equals(PathWaypointKey other)
		{
			if(other == null)
				return false;

			return this.PathId == other.PathId && this.PointId == other.PointId;
		}
	}
}
