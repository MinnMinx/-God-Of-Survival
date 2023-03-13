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

        // Start is called before the first frame update
        void Start()
        {
            if (mainCam == null)
            {
                mainCam = Camera.main;
			}
        }

        // Update is called once per frame
        void Update()
        {
            UpdatePosition();

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
    }
}