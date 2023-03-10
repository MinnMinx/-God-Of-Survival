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
            Atk = 4;
            Basehp = 4;
            Hp = base.GetHp(Basehp);
            Atkrange = 0;
            Speed = 1;
            Atkspeed = 2;
            Cd = 2;           
        }
    }
}
