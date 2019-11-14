using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GladMMO
{
	//Based on Joystick package from Unity3D asset store as well as MMORPG UI 6 package from Dulo Games.
	[RequireComponent(typeof(RectTransform))]
	public class MobileJoystickInputController : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		[SerializeField, Tooltip("The child graphic that will be moved around.")]
		private RectTransform m_Handle;

		public RectTransform Handle
		{
			get => this.m_Handle;
			private set
			{
				this.m_Handle = value;
				UpdateHandle();
			}
		}

		[SerializeField, Tooltip("The handling area that the handle is allowed to be moved in.")]
		private RectTransform m_HandlingArea;
		public RectTransform HandlingArea
		{
			get => this.m_HandlingArea;
			private set => this.m_HandlingArea = value;
		}

		[SerializeField, Tooltip("The child graphic that will be shown when the joystick is active.")]
		private Image m_ActiveGraphic;
		public Image ActiveGraphic
		{
			get => this.m_ActiveGraphic;
			private set => this.m_ActiveGraphic = value;
		}

		[SerializeField] private Vector2 m_Axis;

		[SerializeField, Tooltip("How fast the joystick will go back to the center")]
		private float m_Spring = 25f;
		public float Spring
		{
			get => this.m_Spring;
			private set => this.m_Spring = value;
		}

		[SerializeField, Tooltip("How close to the center that the axis will be output as 0")]
		private float m_DeadZone = 0.1f;
		public float DeadZone
		{
			get => this.m_DeadZone;
			private set => this.m_DeadZone = value;
		}

		private bool m_IsDragging = false;

		protected override void OnEnable()
		{
			base.OnEnable();

			if(this.m_HandlingArea == null)
				this.m_HandlingArea = this.transform as RectTransform;

			if(this.m_ActiveGraphic != null)
				this.m_ActiveGraphic.canvasRenderer.SetAlpha(0f);
		}

		protected void LateUpdate()
		{
			if(!this.m_IsDragging)
			{
				if(this.m_Axis != Vector2.zero)
				{
					Vector2 newAxis = this.m_Axis - (this.m_Axis * Time.unscaledDeltaTime * this.m_Spring);

					if(newAxis.sqrMagnitude <= .0001f)
						newAxis = Vector2.zero;

					this.SetAxis(newAxis);
				}
			}
		}

		public Vector2 JoystickAxis
		{
			get
			{
				Vector2 outputPoint = this.m_Axis.magnitude > this.m_DeadZone ? this.m_Axis : Vector2.zero;
				return outputPoint;
			}
			private set => this.SetAxis(value);
		}

		public void SetAxis(Vector2 axis)
		{
			this.m_Axis = Vector2.ClampMagnitude(axis, 1);

			this.UpdateHandle();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if(!this.IsActive() || this.m_HandlingArea == null)
				return;

			Vector2 newAxis = this.m_HandlingArea.InverseTransformPoint(eventData.position);
			newAxis.x /= this.m_HandlingArea.sizeDelta.x * 0.5f;
			newAxis.y /= this.m_HandlingArea.sizeDelta.y * 0.5f;

			this.SetAxis(newAxis);
			this.m_IsDragging = true;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			this.m_IsDragging = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if(this.m_HandlingArea == null)
				return;

			Vector2 axis = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandlingArea, eventData.position, eventData.pressEventCamera, out axis);

			axis -= this.m_HandlingArea.rect.center;
			axis.x /= this.m_HandlingArea.sizeDelta.x * 0.5f;
			axis.y /= this.m_HandlingArea.sizeDelta.y * 0.5f;

			this.SetAxis(axis);
		}

		private void UpdateHandle()
		{
			if(this.m_Handle && this.m_HandlingArea)
			{
				this.m_Handle.anchoredPosition = new Vector2(this.m_Axis.x * this.m_HandlingArea.sizeDelta.x * 0.5f, this.m_Axis.y * this.m_HandlingArea.sizeDelta.y * 0.5f);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_ActiveGraphic != null)
			{
				this.m_ActiveGraphic.CrossFadeAlpha(1f, 0.2f, false);
			}

			OnBeginDrag(eventData);
			OnDrag(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if(this.m_ActiveGraphic != null)
				this.m_ActiveGraphic.CrossFadeAlpha(0f, 0.2f, false);

			OnEndDrag(eventData);
		}
	}
}
