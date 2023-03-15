using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
	public class Joystick : MonoBehaviour
	{
		[SerializeField]
		private float radiusPx = 111f;
		[SerializeField]
		private float distanceMultipler = 3f;
		[SerializeField]
		private Image InsideBtn;
		[SerializeField]
		private RectTransform parent;
		[SerializeField]
		private CanvasGroup canvasGroup;

		private Rect joystickRange;
		private Vector2 scrToWorld;
		private Camera _camera;

		public void Init(Camera camera)
		{
			if (parent == null)
				parent = transform.parent.GetComponent<RectTransform>();
			joystickRange = parent.rect;
			InsideBtn.rectTransform.anchoredPosition = Vector2.zero;
			_camera = camera;
			scrToWorld = _camera.ScreenSizeWorld() / ScreenUtility.ScreenSizePx;
		}

		public bool Move(Vector2 pixelCord, TouchPhase phase, out Vector2 moved)
		{
			moved = Vector2.zero;
			switch (phase)
			{
				case TouchPhase.Began:
					SetFocus();
					break;
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					InsideBtn.rectTransform.anchoredPosition = Vector2.zero;
					SetFocus(false);
					return false;
			}

			moved = (pixelCord - joystickRange.center).normalized *
								Mathf.Clamp(Vector2.Distance(pixelCord, joystickRange.center), 0, radiusPx);

			InsideBtn.rectTransform.anchoredPosition = moved;
			moved *= scrToWorld * distanceMultipler;

			return true;
		}

		private void SetFocus(bool focus = true)
		{
			canvasGroup.alpha = focus ? 1f : 0.2f;
		}

		public void LoseFocus() => SetFocus(false);
	}
}