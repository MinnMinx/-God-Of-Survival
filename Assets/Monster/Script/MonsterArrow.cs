using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterArrow : Monster
    {
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
    }
}
