using Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.monster
{
    public class monstersword : Monster.monster
    {
        double basehp = 4;
        private new void Start()
        {
            atk = 5;
            hp = GetHp(basehp);
            atkrange = 5;
            speed = 1;
            atkspeed = 2;
            cd = 2;
            base.Start();         
        }

        private new void Update()
        {
            base.Update();
        }
    }
}
