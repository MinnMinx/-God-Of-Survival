using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuKhi
{
    public class SpellBookController : Base
    {
        [SerializeField]
        private Core.PlayerController player;
		[SerializeField]
		private ParticleSystem ps;
		private ParticleSystem.MainModule psMain;
		private ParticleSystem.EmissionModule psEmit;
		private float shootCd;

        private WeaponLeveling leveling;
        [SerializeField]
        private int _level;

		private int stack = 0;
		private float pushBackValue = 0;

		private void Start()
		{
			if (ps != null)
			{
				psMain = ps.main;
				psEmit = ps.emission;
				ps.Play(true);
				shootCd = 1f / base.ATKSpeed;
			}
			_level = 1;
            leveling = new WeaponLeveling();
            leveling.Initialize(1, LevelUp);
		}

		// Update is called once per frame
		void Update()
        {
            if (_level != leveling.Level)
            {
                leveling._ChangeLevel(_level);
			}

            if (player != null && ps != null)
            {
                ps.transform.rotation = Quaternion.Euler(0, 0, player.PlayerAngle);
                shootCd -= Time.deltaTime;
                if (shootCd <= 0)
                {
                    shootCd = 1f / base.ATKSpeed;
                    ps.Play(true);
                }
            }
        }

        void LevelUp(int level)
        {
			ParticleSystem.Burst burst = psEmit.GetBurst(0);
            switch (level)
            {
                case 1:
                    base.ATKBase = 1f;
                    ATKSpeed = 1f;
                    psMain.startLifetime = 0.5f;
					psMain.startSpeed = 8f;
					burst.count = 3;
					psEmit.SetBurst(0, burst);
					pushBackValue = 0;
					break;
                case 2:
                    ATKBase = 1.1f;
					ATKSpeed = 1.2f;
					psMain.startLifetime = 0.6f;
					psMain.startSpeed = 8.3f;
					burst.count = 3;
					psEmit.SetBurst(0, burst);
					pushBackValue = 0;
					break;
                case 3:
					ATKBase = 1.2f;
					ATKSpeed = 1.3f;
					psMain.startLifetime = 0.75f;
					pushBackValue = 0.1f;
					psMain.startSpeed = 8.8f;
					burst.count = 4;
					psEmit.SetBurst(0, burst);
					break;
                case 4:
					ATKBase = 1.4f;
					ATKSpeed = 1.4f;
                    psMain.startLifetime = 0.9f;
					psMain.startSpeed = 9.5f;
					burst.count = 4;
					pushBackValue = 0.2f;
					psEmit.SetBurst(0, burst);
					break;
                case 5:
					ATKBase = 1.6f;
					ATKSpeed = 1.5f;
					psMain.startLifetime = 1f;
					psMain.startSpeed = 12f;
					burst.count = 6;
					psEmit.SetBurst(0, burst);
					pushBackValue = 0.4f;
					break;
            }
        }
		public void Lv5Behavior()
		{
			if (leveling.Level < 5)
				return;

			stack++;
			if (stack >= 3)
			{
				player.HealMissingHp(0.03f);
				stack = 0;
			}
		}

		public void Lv3Behavior(Monster.Monster monster)
		{
			if (leveling.Level < 3)
				return;
			var pos = monster.transform.position;
			monster.transform.position -= (Camera.main.CenterPosition() - pos).normalized * pushBackValue;
		}
	}
}
