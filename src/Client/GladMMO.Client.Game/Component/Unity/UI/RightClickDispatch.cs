using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GladMMO
{
	/// <summary>
	/// Component that can point click events and dispatches them via a <see cref="UnityEvent"/>
	/// only if it's a right-click.
	/// </summary>
	public sealed class RightClickDispatch : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private UnityEvent OnRightClicked;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
				OnRightClicked?.Invoke();
		}
	}
}
