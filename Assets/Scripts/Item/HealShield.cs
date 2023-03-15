using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
	public class HealShield : PickUp
	{
		public override void OnPickUp(PickUpController.PickUpContext context)
		{
			context.player.HealShield(20);
		}
	}
}