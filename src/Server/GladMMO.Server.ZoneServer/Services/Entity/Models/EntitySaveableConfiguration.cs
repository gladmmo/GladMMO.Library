using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class EntitySaveableConfiguration
	{
		private readonly object syncObj = new object();

		private bool _isWorldTeleporting = false;

		/// <summary>
		/// Indicates if the entity position is saveable.
		/// </summary>
		public bool isCurrentPositionSaveable => !isWorldTeleporting;

		/// <summary>
		/// Indicates if the entity is world teleporting.
		/// </summary>
		public bool isWorldTeleporting
		{
			get
			{
				lock (syncObj)
				{
					return _isWorldTeleporting;
				}
			}

			set
			{
				lock (syncObj)
					_isWorldTeleporting = value;
			}
		}
	}
}
