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
		private ParticleSystem.MainModule psMain;
		private ParticleSystem.TextureSheetAnimationModule psAnim;
		private float shootCd;

        private WeaponLeveling leveling;
        [SerializeField]
        private int _level;

		private void Start()
		{
			if (ps != null)
			{
				psMain = ps.main;
				psAnim = ps.textureSheetAnimation;
				ps.Play();
				shootCd = 1f / base.ATKSpeed;
			}
			_level = 1;
            leveling = new SpellBookLeveling();
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
                    ps.Play();
                }
            }
        }

        void LevelUp(int level)
        {
            switch (level)
            {
                case 1:
                    base.ATKBase = 1f;
                    ATKSpeed = 1f;
					psAnim.startFrameMultiplier = 0;
                    psMain.startLifetime = 0.5f;
					psMain.startSpeed = 8f;
					break;
                case 2:
                    ATKBase = 1.1f;
					ATKSpeed = 1.2f;
					psAnim.startFrameMultiplier = 0;
					psMain.startLifetime = 0.6f;
					psMain.startSpeed = 8.3f;
					break;
                case 3:
					ATKBase = 1.2f;
					ATKSpeed = 1.3f;
                    psAnim.startFrameMultiplier = 15f/64f;
					psMain.startLifetime = 0.75f;
					psMain.startSpeed = 8.8f;
					break;
                case 4:
					ATKBase = 1.4f;
					ATKSpeed = 1.4f;
					psAnim.startFrameMultiplier = 15/64f;
                    psMain.startLifetime = 0.9f;
					psMain.startSpeed = 9.5f;
					break;
                case 5:
					ATKBase = 1.6f;
					ATKSpeed = 1.5f;
					psAnim.startFrameMultiplier = 30f/64f;
					psMain.startLifetime = 1f;
					psMain.startSpeed = 12f;
					break;
            }
        }

        public class SpellBookLeveling : WeaponLeveling
        {
			public override void AdditionBehavior() { }
		}
	}

}
