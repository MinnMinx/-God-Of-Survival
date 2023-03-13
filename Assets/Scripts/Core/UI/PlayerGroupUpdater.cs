using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class PlayerGroupUpdater : MonoBehaviour
    {
        [SerializeField]
        private Slider hpSlider;
		[SerializeField]
		private Slider shieldSlider;
		[SerializeField]
		private Core.PlayerController player;

		// Update is called once per frame
		void Update()
        {
            if (player != null)
            {
                hpSlider.value = player.NormalizedHp;
                shieldSlider.value = player.NormalizedShield;
            }
        }
    }
}