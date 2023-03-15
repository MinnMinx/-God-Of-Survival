using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using VuKhi;
using UnityEngine.UIElements;
using Transform = UnityEngine.Transform;

namespace VuKhiPhu
{
    public class FireBallController : Base
    {
        public Transform player;
        public float speed;

        void Update()
        {
            transform.RotateAround(player.position, Vector3.forward, speed * Time.deltaTime);
        }
        // quả cầu lửa nào chạm vào kẻ địch và gây sát thương 
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("ouch");

               // Destroy(gameObject); 

                var enemy = other.gameObject.GetComponent<Monster.Monster>();
                if (enemy != null)
                {
                    enemy.takedamage(ATKBase);
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
