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
            Atkrange = 5;
            Speed = 1;
            Atkspeed = 2;
            Cd = 2;
        }
    }
}
