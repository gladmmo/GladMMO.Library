using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using SceneJect.Common;
using UnityEngine;

namespace GladMMO
{
	[Injectee]
	public sealed class InterestRadiusGatewayEntry : InterestMonitorComponent
	{
		/// <inheritdoc />
		public override void OnTriggerEnter(Collider other)
		{
			GlobalPhysicsEventSystem.Instance.DispatchTriggerEnter(PhysicsTriggerEventType.Interest, gameObject, other);
		}

		/// <inheritdoc />
		public override void OnTriggerExit(Collider other)
		{

		}
	}
}
