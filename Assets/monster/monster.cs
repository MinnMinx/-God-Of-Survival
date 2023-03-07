using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class monster : MonoBehaviour
    {
        Transform destination; // Khai báo biến destination kiểu Vector3

        [SerializeField]
        public float speed ; // Khai báo tốc độ di chuyển của Object

        public int atk;

        public float hp;

        private float heso = 0.5F;

        public float atkrange;

        public int level;

        public float atkspeed;

        public float cd;

        GameObject player;


        public void Start()
        {
            player = GameObject.Find("Circle");
            destination = player.transform;
            level = player.GetComponent<player>().level;
        }

        // Update được gọi mỗi frame
        public void Update()
        {
            float step = speed * Time.deltaTime; // Tính toán khoảng cách Object di chuyển trong mỗi frame
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step); // Di chuyển Object đến vị trí đích

            var distance = Vector3.Distance(transform.position, destination.position);

            cd += Time.deltaTime;
            if (distance <= atkrange && cd >= atkspeed)
            {
                Attack();
            }
        }

        public float GetHp(float basehp)
        {
            float a = (float)Math.Pow(level, heso);
            hp = basehp * a;
            return hp;
        }

        public void Attack()
        {
            Debug.Log("Attack");
            cd = 0;
        }

        public void takedamage(float dame)
        {
            hp = hp - dame;
        }
    }
}