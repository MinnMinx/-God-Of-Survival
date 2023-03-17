using Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Monster
{
    public class MonsterSword : Monster
    {
        private new void Start()
        {
            base.Start();
            Atk = 5;
            Basehp = 4;
            Hp = base.GetHp(Basehp);
            Atkrange = 0.5f;
            Speed = 1.25f;
            Atkspeed = 1;
            Cd = 1;
        }
    }
}
