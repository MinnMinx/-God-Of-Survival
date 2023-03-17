using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuKhi
{
    public class SpellBookController : Base
    {
        [SerializeField]
        private InputController player;
		[SerializeField]
		private ParticleSystem ps;

		private void Start()
		{
            if (ps != null)
            {
                ps.Play();
            }
		}

		// Update is called once per frame
		void Update()
        {
            if (player != null && ps != null)
            {
                ps.transform.rotation = Quaternion.Euler(0, 0, player.PlayerAngle);
            }
        }
	}

}
