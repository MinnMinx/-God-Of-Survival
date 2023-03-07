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

        public double hp;

        private double heso = 0.5;

        public double atkrange;

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

        public double GetHp(double basehp)
        {
            hp = basehp * Math.Pow(level, heso);
            return hp;
        }

        public void Attack()
        {
            Debug.Log("Attack");
            cd = 0;
        }

        public void takedamage()
        {
            Debug.Log("");
        }
    }
}