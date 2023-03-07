using Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace monster
{
    public class monstersword : Monster.monster
    {
        float basehp = 4;
        private new void Start()
        {
            base.Start();
            atk = 5;
            hp = base.GetHp(basehp);
            atkrange = 5;
            speed = 1;
            atkspeed = 2;
            cd = 2;
        }

        private new void Update()
        {
            base.Update();
        }
    }
}
