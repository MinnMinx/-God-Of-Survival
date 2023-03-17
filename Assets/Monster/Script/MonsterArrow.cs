﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterArrow : Monster
    {
        [SerializeField]
        private GameObject arrow;
        [SerializeField]
        private float bulletSp = 7f;

        private new void Start()
        {
            base.Start();
            Atk = 3;
            Basehp = 3;
            Hp = base.GetHp(Basehp);
            Atkrange = 15;
            Speed = 1.25f;
            Atkspeed = 1.25f;
            Cd = 1.25f;
        }
        public override void Attack()
        {
            Vector2 target = new Vector2(Des.position.x, Des.position.y);
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            var newBullet = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, -Vector2.SignedAngle(direction, Vector2.up)));
            var rb = newBullet.GetComponent<Rigidbody2D>();
            newBullet.GetComponent<BulletController>().Atk = Atk;
            //newBullet.transform.LookAt(direction);
            rb.AddForce(direction * bulletSp, ForceMode2D.Impulse);
            base.Cd = 0;    
            Debug.Log("Arrow Attack");
        }
    }
}