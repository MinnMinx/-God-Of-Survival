using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class HealHP : PickUp
    {
		public override void OnPickUp(PickUpController.PickUpContext affectStats)
		{
			//affectStats.Health += 20;
			affectStats.player.Heal(20);
		}
	}

}