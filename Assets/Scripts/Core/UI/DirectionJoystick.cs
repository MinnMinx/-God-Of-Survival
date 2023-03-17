using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
	public class DirectionJoystick : MonoBehaviour
	{
		[SerializeField]
		private float radiusPx = 85f;
		[SerializeField]
		private Image InsideBtn;
		[SerializeField]
		private RectTransform parent;
		[SerializeField]
		private RectTransform indicator;
		[SerializeField]
		private CanvasGroup canvasGroup;

		private Rect joystickRange;

		public void Init()
		{
			if (parent == null)
				parent = transform.parent.GetComponent<RectTransform>();
			joystickRange = parent.rect;
			joystickRange.x += Screen.width;
			InsideBtn.rectTransform.anchoredPosition = Vector2.zero;
		}

		public bool Rotate(Vector2 pixelCord, TouchPhase phase, out float angle)
		{
			angle = 0;
			switch (phase)
			{
				case TouchPhase.Began:
					SetFocus();
					break;
				case TouchPhase.Stationary:
					return false;
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					InsideBtn.rectTransform.anchoredPosition = Vector2.zero;
					SetFocus(false);
					return false;
			}

			Vector2 moved = (pixelCord - joystickRange.center).normalized *
							Mathf.Clamp(Vector2.Distance(pixelCord, joystickRange.center), 0, radiusPx);
			angle = -Vector2.SignedAngle(moved, Vector2.right);
			indicator.rotation = Quaternion.Euler(0, 0, angle);
			InsideBtn.rectTransform.anchoredPosition = moved;

			return true;
		}

		public Vector2 center => joystickRange.center;

		private void SetFocus(bool focus = true)
		{
			canvasGroup.alpha = focus ? 1f : 0.2f;
		}

		public void LoseFocus() => SetFocus(false);
	}
}