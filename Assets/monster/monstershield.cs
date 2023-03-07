using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class monstershield : monster
    {
        float basehp = 4;
        private new void Start()
        {
            base.Start();
            atk = 4;
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
