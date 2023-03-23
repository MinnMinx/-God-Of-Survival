using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuKhi
{
    public class WeaponLeveling
    {
        private int level;
        private int currentExp;
        private Action<int> levelUpAction;

        public int ExpTillNextLv => level; // lv5 need 5 exp
        public int CurrentExp => currentExp;
        public int Level => level;

        public void Initialize(int level = 1, Action<int> levelUpAction = null)
        {
            this.level = level;
            currentExp = 0;
            this.levelUpAction = levelUpAction;
            levelUpAction(level);
		}

        public void GetExp(int value)
        {
            currentExp += value;
            while (currentExp >= ExpTillNextLv)
            {
                var remain = currentExp - ExpTillNextLv;
                currentExp = remain;
                level++;
				if (levelUpAction != null)
                {
                    levelUpAction(level);
                }
            }
		}
	}
}