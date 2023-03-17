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
        private float bulletSp = 10f;

        private int numbers = 3;
        private new void Start()
        {
            base.Start();
            Atk = 1;
            Basehp = 3;
            Hp = base.GetHp(Basehp);
            Atkrange = 20;
            Speed = 1;
            Atkspeed = 1.5f;
            Cd = 1.5f;
        }

        public override void Attack()
        {
            Vector2 target = new Vector2(Des.position.x, Des.position.y);
            for (int i = 0;i < numbers; i++)
            {
                var newBullet = Instantiate(bullet, transform.position, transform.rotation);
                var rb = newBullet.GetComponent<Rigidbody2D>();
                newBullet.GetComponent<BulletController>().Atk = Atk;
                Vector2 direction = (target - (Vector2)transform.position).normalized;
                rb.AddForce(direction * bulletSp, ForceMode2D.Impulse);
            }           
            base.Cd = 0;
            Debug.Log("Gun Attack");
        }
    }
}
