using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class DemoLocalPlayerMovement : MonoBehaviour
	{
		[SerializeField]
		private float Speed = 3;

		void Update()
		{
			var x = Input.GetAxisRaw("Horizontal");
			var z = Input.GetAxisRaw("Vertical");

			Vector3 movementDirection = (transform.right * x + transform.forward * z);
			
			//Remove any potential Y value for safety
			movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z).normalized;

			this.transform.position += movementDirection * Time.deltaTime * Speed;
		}
	}
}
