using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GladMMO.Database;

namespace GladMMO
{
	/// <summary>
	/// EF table model for waypoint paths.
	/// </summary>
	[Table("path_waypoints")]
	public class PathWaypointModel
	{
		/// <summary>
		/// The ID of the path this waypoint logically belongs to.
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int PathId { get; private set; }

		/// <summary>
		/// The id of the point.
		/// (Ex. A path may have many points in it, this id represents that order)
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int PointId { get; private set; }

		/// <summary>
		/// The actual point.
		/// </summary>
		[Required]
		public Vector3<float> Point { get; private set; }

		/// <inheritdoc />
		public PathWaypointModel(int pathId, int pointId, Vector3<float> point)
		{
			if(pathId < 0) throw new ArgumentOutOfRangeException(nameof(pathId));
			if(pointId < 0) throw new ArgumentOutOfRangeException(nameof(pointId));

			PathId = pathId;
			PointId = pointId;
			Point = point ?? throw new ArgumentNullException(nameof(point));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected PathWaypointModel()
		{

		}
	}
}
