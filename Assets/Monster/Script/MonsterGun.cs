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
        private float bulletSp = 10f;

        private new void Start()
        {
            base.Start();
            Atk = 2;
            Basehp = 3;
            Hp = base.GetHp(Basehp);
            Atkrange = 20;
            Speed = 1;
            Atkspeed = 1/0.35f;
            Cd = Atkspeed;

            if (Tinhanh)
            {
                Atk = Atk * 1.5f;
                Hp = Hp * 1.3f;
                Atkspeed = 1/0.7f;
                Cd = Atkspeed;
                gameObject.transform.localScale = new Vector3(2, 2, 2);
                //Debug.Log("tinh anh spawn");
            }
        }

        public override void Attack()
        {
            Vector2 target = new Vector2(Des.position.x, Des.position.y);
            var newBullet = Instantiate(bullet, transform.position, transform.rotation);
            var rb = newBullet.GetComponent<Rigidbody2D>();
            newBullet.GetComponent<BulletController>().Atk = Atk;
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            rb.AddForce(direction * bulletSp, ForceMode2D.Impulse);
            base.Cd = 0;
            //Debug.Log("Gun Attack");
        }
    }
}
