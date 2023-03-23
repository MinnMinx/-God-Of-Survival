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
            Speed = 2.5f;
            Atkspeed = 1;
            Cd = 1;

            if (Tinhanh)
            {
                Atk = Atk * 1.1f;
                Hp = Hp * 1.4f;
                speed = speed * 1.4f;
                gameObject.transform.localScale = new Vector3(4, 4, 2);
                //Debug.Log("tinh anh spawn");
            }
        }
    }
}
