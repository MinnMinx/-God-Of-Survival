using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class monster : MonoBehaviour
    {
        public Transform destination; // Khai báo biến destination kiểu Vector3

        public float speed = 1F; // Khai báo tốc độ di chuyển của Object

        void Start()
        {
            destination = GameObject.Find("Circle").transform;
        }

        // Update được gọi mỗi frame
        void Update()
        {
            float step = speed * Time.deltaTime; // Tính toán khoảng cách Object di chuyển trong mỗi frame
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step); // Di chuyển Object đến vị trí đích
        }
    }
}