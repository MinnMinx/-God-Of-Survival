using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Monster
{
    public class MonsterGun : Monster
    {
        [SerializeField]
        private GameObject bullet;

        [SerializeField]
        private float bulletSp = 8f;

        private new void Start()
        {
            base.Start();
            Atk = 2;
            Basehp = 3;
            Hp = base.GetHp(Basehp);
            Atkrange = 15;
            Speed = 1;
            Atkspeed = 1/0.5f;
            Cd = Atkspeed;

            if (Tinhanh)
            {
                Atk = Atk * 1.5f;
                Hp = Hp * 1.3f;
                Atkspeed = 1/1f;
                Cd = Atkspeed;
                gameObject.transform.localScale = new Vector3(4, 4, 2);
                //Debug.Log("tinh anh spawn");
            }
        }

        public override void Attack()
        {
            Vector2 target = new Vector2(Des.position.x, Des.position.y);
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            var newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -Vector2.SignedAngle(direction, Vector2.up)));
            var rb = newBullet.GetComponent<Rigidbody2D>();
            newBullet.GetComponent<BulletController>().Atk = Atk;
            rb.AddForce(direction * bulletSp, ForceMode2D.Impulse);
            base.Cd = 0;
            //Debug.Log("Gun Attack");
        }
    }
}
