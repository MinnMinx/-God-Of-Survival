using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterMage : Monster
    {
        [SerializeField]
        private GameObject bullet;

        [SerializeField]
        private float bulletSp = 8f;

        private int numbers = 3;
        private new void Start()
        {
            base.Start();
            Atk = 1;
            Basehp = 3;
            Hp = base.GetHp(Basehp);
            Atkrange = 15;
            Speed = 2.5f;
            Atkspeed = 1/0.5f;
            Cd = 1.5f;

            if (Tinhanh)
            {
                Atk = Atk * 1.4f;
                Atkspeed = 1 / 0.75f;
                Cd = Atkspeed;
                numbers = 5;
                gameObject.transform.localScale = new Vector3(4, 4, 2);
                //Debug.Log("tinh anh spawn");
            }
        }

        public override void Attack()
        {
            Vector2 target = new Vector2(Des.position.x, Des.position.y);
            Vector2 direction = (target - (Vector2)transform.position);
            direction = direction.normalized;
            float degree = 15f;
            Quaternion rotation = Quaternion.Euler(0, 0, 15);
            for (int i = 0;i < numbers; i++)
            {
                var newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -Vector2.SignedAngle(direction, Vector2.up)));
                var rb = newBullet.GetComponent<Rigidbody2D>();
                newBullet.GetComponent<BulletController>().Atk = Atk;              
                rb.AddForce(direction * bulletSp, ForceMode2D.Impulse);
                direction =  rotation * direction;
                degree = i > 1 ? degree - 30 : degree;
            }           
            base.Cd = 0;
        }
    }
}
