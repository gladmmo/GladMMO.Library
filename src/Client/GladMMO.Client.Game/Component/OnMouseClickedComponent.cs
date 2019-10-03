using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO.Component
{
	public sealed class OnMouseClickedComponent : MonoBehaviour
	{
		public event EventHandler OnMouseClicked;

		private void OnMouseDown()
		{
			OnMouseClicked?.Invoke(this, EventArgs.Empty);
		}
	}
}
