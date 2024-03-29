﻿using Core;
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
		private Transform weaponSpr;
		[SerializeField]
		private ParticleSystem ps;
		private ParticleSystem.MainModule psMain;
		private ParticleSystem.EmissionModule psEmit;
		private float shootCd;

        private WeaponLeveling leveling;

		private int stack = 0;
		private float pushBackValue = 0;

		public float PlayerAtk => player.BaseAttack;

		public override void Init()
		{
			if (ps != null)
			{
				psMain = ps.main;
				psEmit = ps.emission;
				ps.Play(true);
				shootCd = 1f / base.ATKSpeed;
			}
            leveling = new WeaponLeveling();
            leveling.Initialize(1, LevelUp);
			player.SetWeaponSpriteObject(weaponSpr);
		}

		// Update is called once per frame
		void Update()
        {
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
                    psMain.startLifetime = 0.8f;
					psMain.startSpeed = 8.5f;
					burst.count = 3;
					psEmit.SetBurst(0, burst);
					pushBackValue = 0;
					break;
                case 2:
                    ATKBase = 1.1f;
					ATKSpeed = 1.2f;
					psMain.startLifetime = 0.95f;
					psMain.startSpeed = 8.8f;
					burst.count = 3;
					psEmit.SetBurst(0, burst);
					pushBackValue = 0;
					break;
                case 3:
					ATKBase = 1.2f;
					ATKSpeed = 1.3f;
					psMain.startLifetime = 1.1f;
					pushBackValue = 0.1f;
					psMain.startSpeed = 9.1f;
					burst.count = 4;
					psEmit.SetBurst(0, burst);
					break;
                case 4:
					ATKBase = 1.4f;
					ATKSpeed = 1.4f;
                    psMain.startLifetime = 1.3f;
					psMain.startSpeed = 9.5f;
					burst.count = 4;
					pushBackValue = 0.2f;
					psEmit.SetBurst(0, burst);
					break;
                case 5:
					ATKBase = 1.6f;
					ATKSpeed = 1.5f;
					psMain.startLifetime = 1.5f;
					psMain.startSpeed = 12f;
					burst.count = 6;
					psEmit.SetBurst(0, burst);
					pushBackValue = 0.4f;
					break;
            }
			if (level > 5)
			{
				ATKBase += 2f * (level - 5);
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

		public override void GetExp(int value = 1)
		{
			base.GetExp(value);
			leveling.GetExp(value);
		}

		public override bool IsMaxLevel()
		{
			return leveling.Level >= 5;
		}

		public override float CurrentExp
		{
			get => (float)leveling.CurrentExp / leveling.ExpTillNextLv;
		}

		public override float Level => leveling.Level;
	}
}
