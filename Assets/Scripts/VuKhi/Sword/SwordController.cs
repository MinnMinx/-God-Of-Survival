using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace VuKhi
{
    public class SwordController : Base
    {
        [SerializeField]
        private Transform parent;
		[SerializeField]
		private Core.PlayerController player;
		[SerializeField]
		private SwordFxController fx;
		private WeaponLeveling leveling;
		float time;

        // Start is called before the first frame update
        public override void Init()
        {
            time = 0;
			leveling = new WeaponLeveling();
			leveling.Initialize(1, LevelUp);
        }

        // Update is called once per frame

        void Update()
		{
			fx.UpdateRotation(player.PlayerAngle);
			time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 1 / ATKSpeed;
                fx.Swing();
			}
        }

        public float Damage => ATKBase + player.BaseAttack;

		void LevelUp(int level)
		{
			switch (level)
			{
				case 1:
					base.ATKBase = 3f;
					ATKSpeed = 1f;
					break;
				case 2:
					base.ATKBase = 3.2f;
					ATKSpeed = 1.1f;
					break;
				case 3:
					base.ATKBase = 3.5f;
					ATKSpeed = 1.2f;
					break;
				case 4:
					base.ATKBase = 3.8f;
					ATKSpeed = 1.3f;
					break;
				case 5:
					base.ATKBase = 4f;
					ATKSpeed = 1.5f;
					break;
			}
			fx.LevelUp(level);
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

