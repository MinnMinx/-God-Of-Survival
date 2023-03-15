using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterMage : Monster
    {
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
            Debug.Log("Mage Attack");
        }
    }
}
