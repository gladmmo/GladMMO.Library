using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO.Component
{
	public sealed class MouseButtonClickEventArgs : EventArgs
	{
		public enum MouseType
		{
			Unknown = 0,
			Left = 1,
			Right = 2
		}

		public MouseType Type { get; }

		public MouseButtonClickEventArgs(MouseType type)
		{
			Type = type;
		}
	}

	public sealed class OnMouseClickedComponent : MonoBehaviour
	{
		public event EventHandler<MouseButtonClickEventArgs> OnMouseClicked;

		//Can't deal with right clicks on MouseClick callback
		private void OnMouseOver()
		{
			if(Input.GetMouseButtonDown(0))
			{
				OnMouseClicked?.Invoke(this, new MouseButtonClickEventArgs(MouseButtonClickEventArgs.MouseType.Left));
			}
			else if (Input.GetMouseButtonDown(1))
			{
				OnMouseClicked?.Invoke(this, new MouseButtonClickEventArgs(MouseButtonClickEventArgs.MouseType.Right));
			}
		}
	}
}
