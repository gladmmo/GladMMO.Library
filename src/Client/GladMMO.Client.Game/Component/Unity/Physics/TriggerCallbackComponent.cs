using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class TriggerCallbackComponent : MonoBehaviour, IPhysicsTrigger
	{
		/// <summary>
		/// The editor-exposed serialized <see cref="BoxCollider"/> field.
		/// </summary>
		[SerializeField]
		private BoxCollider _Collider;

		public BoxCollider Collider => _Collider;

		/// <summary>
		/// Event published when a collider on the same layer enter the trigger.
		/// </summary>
		public event EventHandler<Transform> OnTriggerEntered;

		/// <summary>
		/// Event published when a collider on the same layer exits the trigger.
		/// </summary>
		public event EventHandler<Transform> OnTriggerExited;

		public void OnTriggerEnter(Collider enteredCollider)
		{
			OnTriggerEntered?.Invoke(this, enteredCollider.GetRootGameObject().transform);
		}

		public void OnTriggerExit(Collider exitedCollider)
		{
			OnTriggerExited?.Invoke(this, exitedCollider.GetRootGameObject().transform);
		}
	}
}
