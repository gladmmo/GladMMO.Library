using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using SceneJect.Common;
using UnityEngine;

namespace GladMMO
{
	[Injectee]
	public sealed class InterestRadiusGatewayExit : InterestMonitorComponent
	{
		/// <inheritdoc />
		public override void OnTriggerEnter(Collider other)
		{
			//We don't do anything on entry since we need staggered exit/enter.
		}

		/// <inheritdoc />
		public override void OnTriggerExit(Collider other)
		{
			GlobalPhysicsEventSystem.Instance.DispatchTriggerExit(PhysicsTriggerEventType.Interest, gameObject, other);
		}
	}
}
