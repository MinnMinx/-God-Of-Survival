using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStat stats;
        private PlayerLeveling playerLeveling;

        private SpriteRenderer spr;

        public float BaseAttack => stats.baseAtk;
        public bool IsDead => stats.health <= 0;
        public int Level => playerLeveling.Level;
        public float NormalizedExpProgress => playerLeveling.ExpProgress;

		// Start is called before the first frame update
		void Start()
        {
            playerLeveling = new PlayerLeveling(this.OnLevelUp);
            spr = GetComponent<SpriteRenderer>();
            OnLevelUp(0);
		}

        private void OnLevelUp (int level)
        {
            stats.baseAtk = 0.125f * level;
        }

        public Vector2 Move(float deltaX, float deltaY)
        {
            var position = transform.position;
            position.x += deltaX * stats.baseSpeed * stats.speedMultipler;
            position.y += deltaY * stats.baseSpeed * stats.speedMultipler;
            transform.position = position;
            return position;
        }

        public void FlipSprite(bool right = true) => spr.flipX = right;

		public void TakeDamage(float value)
        {
            if (stats.shield > value)
            {
                stats.shield -= value;
            }
            else if (stats.shield > 0)
            {
                float damageLeft = value - stats.shield;
                stats.shield = 0;
                stats.health -= damageLeft;
            }
            else
            {
                stats.health -= value;
            }
        }

        public void ReceiveExp(float value = 1) => playerLeveling.ReceiveExp(value);

        public class PlayerLeveling
        {
            private int level;
            private float currentExp;
            private float expUntilLevelUp => 10 * Mathf.Pow(level, 0.7f);

            public int Level => level;
            public float CurrentExp => currentExp;
            public float ExpProgress => currentExp / expUntilLevelUp;
            private Action<int> OnLevelUp { get; set; }

            public PlayerLeveling(Action<int> OnLevelUp)
            {
                level = 0;
                currentExp = 0;
                this.OnLevelUp = OnLevelUp;
			}
            public PlayerLeveling() : this((i) => { }) { }

            public void ReceiveExp(float value)
            {
                currentExp += value;
                if (currentExp > expUntilLevelUp)
                {
                    // Level up
                    float extraExp = currentExp -= expUntilLevelUp;
                    level++;
                    currentExp = extraExp;
                    OnLevelUp(level);
                }
			}
		}

		[Serializable]
		public class PlayerStat
		{
			public float health = 100f;
			public float shield = 100f;
			public float baseAtk = 1f;
			public float baseSpeed = 1f;
			public float speedMultipler = 1f;
		}
	}
}