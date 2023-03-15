using Core.UI;
using System.Collections;
using System.Collections.Generic;
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
        private int moveTouchId = -1, dirTouchId = -1;

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
#if UNITY_EDITOR
			moveJoystick.LoseFocus();
			dirJoystick.LoseFocus();
#endif
		}

		// Update is called once per frame
		void Update()
        {
			bool isMoved = false, isDired = false;
			int tempMoveId = -1, tempDirMoveId = -1;
			for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (!isMoved)
				{
					if (touch.fingerId == moveTouchId &&
						moveJoystick.Move(touch.position, touch.phase, out Vector2 moved))
					{
						_MovePosition(moved.x * Time.deltaTime, moved.y * Time.deltaTime);
						isMoved = true;
					}
					else if (touch.phase == TouchPhase.Ended ||
							touch.phase == TouchPhase.Canceled)
					{
						moveTouchId = -1;
					}
					else if (touch.rawPosition.x < Screen.width / 2f)
					{
						tempMoveId = i;
					}
					continue;
				}
				
				if (!isDired)
				{
					if (touch.fingerId == dirTouchId &&
						dirJoystick.Rotate(touch.position, touch.phase, out float angle))
					{
						_Rotate(angle);
						isDired = true;
					}
					else if (touch.phase == TouchPhase.Ended ||
							touch.phase == TouchPhase.Canceled)
					{
						dirTouchId = -1;
					}
					else if (touch.rawPosition.x >= Screen.width / 2f)
					{
						tempDirMoveId = i;
					}
				}
            }


			if (tempMoveId > 0)
            {
                Touch touchMove = Input.GetTouch(tempMoveId);
                moveTouchId = touchMove.fingerId;
                if (moveJoystick.Move(touchMove.position, touchMove.phase, out Vector2 moved))
					_MovePosition(moved.x * Time.deltaTime, moved.y * Time.deltaTime);
			}

			if (tempDirMoveId > 0)
			{
				Touch touchDir = Input.GetTouch(tempDirMoveId);
				dirTouchId = touchDir.fingerId;
                if (dirJoystick.Rotate(touchDir.position, touchDir.phase, out float angle))
					_Rotate(angle);
			}

//#if UNITY_EDITOR
//			UpdatePosition();
//            if (Input.GetMouseButton(0) && Input.mousePosition.x >= Screen.width / 2f)
//            {
//				if (dirJoystick.Rotate(Input.mousePosition, TouchPhase.Began, out float angle))
//					_Rotate(angle);
//			}
//#endif
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