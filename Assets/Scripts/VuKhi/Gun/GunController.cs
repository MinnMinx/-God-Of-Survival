using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuKhi
{
    public class GunController : Base
    {
        // Start is called before the first frame update
        public Core.PlayerController player;
        public Transform SprParent;
        public GameObject bulletPrefab, bulletPrafabLv3, bulletPrefabLv5, bulletSpecialPrefab;
		public ParticleSystem fireFX;
		private ParticleSystemRenderer fireFxRenderer;
		public Vector3 firePosition;
        public float bulletSpeed = 10f; // tốc độ của đạn
		public float fireCdEach = 0.5f;
        private float lastFireTime;
		private int numBulletFire = 1;
		private WeaponLeveling leveling;
		private GameObject _prefabToSpawn;
		private int stack = 0;

		bool isSpecialShot => leveling.Level >= 5 && stack >= 5*3;

		public void Shoot()
        {
            //"bulletPrefab" là prefab cho viên đạn
            // "bulletSpawn" là vị trí khởi đầu của viên đạn.
            // Khi phương thức Shoot được gọi, chúng ta sẽ tạo một instance của prefab đạn tại vị trí và hướng của "bulletSpawn".
            if (fireFX != null)
            {
                fireFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                fireFX.Play(true);
			}
			bool isSpecial = isSpecialShot;
			float speed = bulletSpeed;
			float dmg = player.BaseAttack + base.ATKBase;
			GameObject gameObj;
			if (isSpecialShot)
			{
				gameObj = Instantiate(bulletSpecialPrefab, transform.TransformPoint(firePosition), Quaternion.Euler(0, 0, player.PlayerAngle));
				dmg = player.BaseAttack + base.ATKBase * 2;
				speed /= 2;
				stack = 0;
			}
			else
			{
				gameObj = Instantiate(_prefabToSpawn, transform.TransformPoint(firePosition), Quaternion.Euler(0, 0, player.PlayerAngle));
				if (leveling.Level >= 5)
				{
					stack++;
				}
			}
            float angleRad = player.PlayerAngle * Mathf.Deg2Rad;
            gameObj.GetComponent<BulletController>().Init(dmg, speed * new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)), isSpecial);
        }

		public override void Init()
		{
            player.SetWeaponSpriteObject(SprParent);
            if (fireFX != null)
            {
				fireFxRenderer = fireFX.GetComponent<ParticleSystemRenderer>();
            }
			leveling = new WeaponLeveling();
			leveling.Initialize(1, LevelUp);
			stack = 0;
		}

		// Update is called once per frame
		void Update()
        {
            if (base.ATKSpeed > 0 && Time.time - lastFireTime > 1f / base.ATKSpeed)
            {
				if (isSpecialShot)
				{
					Shoot();
				}
				else
				{
					for (int i = 0; i < numBulletFire; i++)
					{
						Invoke("Shoot", fireCdEach * i);
					}
				}
                lastFireTime = Time.time;
            }
            if (SprParent.localScale.x >= 0)
            {
                SprParent.rotation = Quaternion.Euler(0, 0, player.PlayerAngle);
                if (fireFxRenderer != null)
                {
                    fireFxRenderer.flip = Vector3.zero;
				}
            } else
            {
				SprParent.rotation = Quaternion.Euler(0, 0, 180 + player.PlayerAngle);
				if (fireFxRenderer != null)
				{
					fireFxRenderer.flip = Vector3.right;
				}
			}
        }
		void LevelUp(int level)
		{
			switch (level)
			{
				case 1:
					base.ATKBase = 2f;
					ATKSpeed = 1f;
					numBulletFire = 1;
					_prefabToSpawn = bulletPrefab;
					break;
				case 2:
					ATKBase = 2.2f;
					ATKSpeed = 1f;
					numBulletFire = 1;
					_prefabToSpawn = bulletPrefab;
					break;
				case 3:
					ATKBase = 2.3f;
					ATKSpeed = 1.1f;
					numBulletFire = 2;
					_prefabToSpawn = bulletPrafabLv3;
					break;
				case 4:
					ATKBase = 2.5f;
					ATKSpeed = 1.2f;
					numBulletFire = 2;
					_prefabToSpawn = bulletPrafabLv3;
					break;
				case 5:
					ATKBase = 2.7f;
					ATKSpeed = 1.3f;
					numBulletFire = 3;
					_prefabToSpawn = bulletPrefabLv5;
					break;
			}
			if (level > 5)
			{
				ATKBase += 2f * (level - 5);
			}
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
			get => (float)leveling.CurrentExp/leveling.ExpTillNextLv;
		}

		public override float Level => leveling.Level;
	}
}
