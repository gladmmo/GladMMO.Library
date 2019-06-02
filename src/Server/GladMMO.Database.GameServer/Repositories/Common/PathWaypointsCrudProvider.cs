using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	//We have to override Find because we have a complex key
	public sealed class PathWaypointsCrudProvider : GeneralGenericCrudRepositoryProvider<PathWaypointKey, PathWaypointModel>
	{
		/// <inheritdoc />
		public PathWaypointsCrudProvider(DbSet<PathWaypointModel> modelSet, DbContext context) 
			: base(modelSet, context)
		{

		}

		/// <inheritdoc />
		public override Task<PathWaypointModel> RetrieveAsync(PathWaypointKey key)
		{
			return ModelSet.FindAsync(key.PathId, key.PointId);
		}
	}
}
