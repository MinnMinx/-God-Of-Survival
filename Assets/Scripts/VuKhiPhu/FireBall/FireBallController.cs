using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using VuKhi;
using UnityEngine.UIElements;
using Transform = UnityEngine.Transform;
using System;
using System.Threading;
using Item;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

namespace VuKhiPhu
{
    public class FireBallController : Base
    {
        private Transform player;
        public float speed;
        private bool active = false;
        private static int count = 0;
        public float orbitDistance = 3.0f;

        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        void Update()
        {
            Orbit();

        }

        void LateUpdate()
        {

            Orbit();

        }

        void Orbit()
        {
            if (active == true)
            {
                transform.position = player.position + (transform.position - player.position).normalized * orbitDistance;
                transform.RotateAround(player.position, Vector3.forward, speed * Time.deltaTime);

            }
        }

        // quả cầu lửa nào chạm vào kẻ địch và gây sát thương 
        void OnTriggerEnter2D(Collider2D other)
        {
            // Destroy(gameObject); 

            var enemy = other.gameObject.GetComponent<Monster.Monster>();
            if (enemy != null)
            {
                enemy.takedamage(ATKBase);
            }

            var player = other.gameObject.GetComponent<Core.PlayerController>();
            if (player != null)
            {
                if (count < 6)
                {
                    active = true;
                    count++;
                }
                else
                {
                    player.Heal(20);
                    Destroy(gameObject);
                }
            }
            



        }
        //phương thức để sử dụng phép thuật của nhân vật, sử dụng một vòng lặp để sinh ra các quả cầu lửa.
        //void CastSpell()
        //{
        //    for (int i = 0; i < numFireballs; i++)
        //    {
        //        float angle = i * Mathf.PI * 2 / numFireballs; // tính góc giữa các quả cầu lửa
        //        Vector3 pos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius; // tính vị trí của quả cầu lửa trên đường tròn

        //        GameObject fireball = Instantiate(fireballPrefab, pos, Quaternion.identity); // sinh ra quả cầu lửa
        //        fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed; // cho quả cầu lửa di chuyển theo hướng của nó

        //        // set damage tương ứng cho quả cầu lửa
        //        fireball.GetComponent<Fireball>().damage = ATK;
        //    }
        //}
    }

}
