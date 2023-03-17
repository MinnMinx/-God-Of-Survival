using Core.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Core
{
    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController player;
        [SerializeField]
        private MapController map;
        [SerializeField]
        private Camera mainCam;
		[SerializeField]
		private Joystick moveJoystick;
		[SerializeField]
		private DirectionJoystick dirJoystick;
        private int moveTouchId, dirTouchId;

		public float PlayerAngle { get; private set; }

		// Start is called before the first frame update
		void Start()
        {
            if (mainCam == null)
            {
                mainCam = Camera.main;
			}
            moveJoystick.Init(mainCam);
            dirJoystick.Init();
			moveTouchId = -1;
			dirTouchId = -1;
#if UNITY_EDITOR
			moveJoystick.LoseFocus();
			dirJoystick.LoseFocus();
#endif
		}

		// Update is called once per frame
		void Update()
        {
			if (Input.touchCount > 0)
			{
				Touch moveTouch = default;
				Touch dirTouch = default;
				bool noMove = moveTouchId < 0;
				bool noDir = dirTouchId < 0;

				for (int i = 0; i < Input.touchCount; i++)
				{
					Touch temp = Input.GetTouch(i);
					if (temp.fingerId == moveTouchId)
					{
						moveTouch = temp;
						noMove = false;
					}
					else if (temp.fingerId == dirTouchId)
					{
						dirTouch = temp;
						noDir = false;
					}

					if (noMove &&
						temp.phase == TouchPhase.Began &&
						temp.rawPosition.x < Screen.width / 2f)
					{
						moveTouch = temp;
						moveTouchId = moveTouch.fingerId;
						noMove = false;
					}
					else if (noDir && temp.phase == TouchPhase.Began)
					{
						dirTouch = temp;
						dirTouchId = dirTouch.fingerId;
						noDir = false;
					}
				}

				if (!noMove &&
					moveTouchId == moveTouch.fingerId &&
					moveJoystick.Move(moveTouch.position, moveTouch.phase, out Vector2 moved))
				{
					_MovePosition(moved.x * Time.deltaTime, moved.y * Time.deltaTime);
				}

				if (!noDir &&
					dirTouchId == dirTouch.fingerId &&
					dirJoystick.Rotate(dirTouch.position, dirTouch.phase, out float angle))
				{
					_Rotate(angle);
					PlayerAngle = angle;
				}
			}

#if UNITY_EDITOR
			UpdatePosition();
			if (Input.GetMouseButton(0) && Input.mousePosition.x >= Screen.width / 2f)
			{
				if (dirJoystick.Rotate(Input.mousePosition, TouchPhase.Began, out float angle_))
					_Rotate(angle_);
				PlayerAngle = angle_;
			}
#endif
		}
		private void LateUpdate()
		{
			if (Input.touchCount > 0)
			{
				bool hasMoveTouch = false, hasDirTouch = false;
				for (int i = 0; i < Input.touchCount; i++)
				{
					Touch temp = Input.GetTouch(i);
					if (temp.fingerId == moveTouchId)
					{
						hasMoveTouch = temp.phase != TouchPhase.Ended && temp.phase != TouchPhase.Canceled;
					}
					else if (temp.fingerId == dirTouchId)
					{
						hasDirTouch = temp.phase != TouchPhase.Ended && temp.phase != TouchPhase.Canceled;
					}
				}

				if (!hasMoveTouch) moveTouchId = -1;
				if (!hasDirTouch) dirTouchId = -1;
			}
		}
		void UpdatePosition()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
			_MovePosition(horizontal * Time.deltaTime, vertical * Time.deltaTime);
		}
        void _MovePosition(float x, float y)
        {
            var moved = player.Move(x, y);
			map.Move(moved.x, moved.y);
            var camPos = mainCam.transform.position;
            camPos.x = moved.x;
            camPos.y = moved.y;
            mainCam.transform.position = camPos;
		}
        void _Rotate(float angle)
        {
            // Rotate Weapon
            player.FlipSprite(Mathf.Abs(angle) > 90);
        }
    }
}