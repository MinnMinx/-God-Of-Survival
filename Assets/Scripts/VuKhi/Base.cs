using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VuKhi
{
    public class Base : MonoBehaviour
    {
        public float ATKBase = 3;
        public float ATKSpeed = 1;

        public virtual void GetExp(int value = 1) { }
        public virtual bool IsMaxLevel()
        {
            return false;
        }
		public virtual float CurrentExp { get; }
		public virtual float Level { get; }
        public virtual void Init() { }
	}
}
