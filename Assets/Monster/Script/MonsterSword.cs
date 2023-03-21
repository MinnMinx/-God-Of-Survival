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
            Atkrange = 0f;
            Speed = 1.25f;
            Atkspeed = 1;
            Cd = 1;

            if (Tinhanh)
            {
                Atk = Atk * 1.3f;
                Hp = Hp * 1.4f;
                speed = speed * 1.3f;
                gameObject.transform.localScale = new Vector3(4, 4, 2);
                //Debug.Log("tinh anh spawn");
            }
        }
    }
}
