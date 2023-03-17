using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterShield : Monster
    {
        private new void Start()
        {
            base.Start();
            Atk = 3;
            Basehp = 6;
            Hp = base.GetHp(Basehp);
            Atkrange = 0;
            Speed = 1.25f;
            Atkspeed = 1;
            Cd = 1;           
        }
    }
}
