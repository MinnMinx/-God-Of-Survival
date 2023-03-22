using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStat stats;
        private PlayerLeveling playerLeveling;

        [SerializeField]
        private SpriteRenderer spr;
		[SerializeField]
		private Transform addtionFlipSpr;
		[SerializeField]
		private ParticleSystem shieldBreak;
		[SerializeField]
        private Animator anim;

        public float BaseAttack => stats.baseAtk * stats.atkMultipler;
        public float AttackMultipler => stats.atkMultipler;
        public bool IsDead => stats.Health <= 0;
        public int Level => playerLeveling.Level;
        public float NormalizedHp => stats.Health / 100f;
        public float NormalizedShield => stats.Shield / 100f;
        public float NormalizedExpProgress => playerLeveling.ExpProgress;
        public float PlayerAngle { get; internal set; }
        private float Score = 0;

        private void Update()
        {
            Score += Time.deltaTime;
            if (IsDead)
            {
                SceneManager.LoadScene(2);
            }
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat("score", Score);
            PlayerPrefs.SetInt("level", Level);
        }

        // Start is called before the first frame update
        void Start()
        {
            playerLeveling = new PlayerLeveling(this.OnLevelUp);
            if (spr == null)
                spr = GetComponent<SpriteRenderer>();
            OnLevelUp(1);
        }

        private void OnLevelUp(int level)
        {
            stats.baseAtk = 0.125f * level;
        }

        public Vector2 Move(float deltaX, float deltaY)
        {
            if (anim != null && !anim.GetBool("running"))
            {
                anim.SetBool("running", true);
            }
            var position = transform.position;
            position.x += deltaX * stats.baseSpeed * stats.speedMultipler;
            position.y += deltaY * stats.baseSpeed * stats.speedMultipler;
            transform.position = position;
            return position;
        }

        public void StopRunning() => _TurnOffBool("running");

        void _TurnOffBool(string name)
        {
            if (anim != null)
                anim.SetBool(name, false);
        }
        void _StopHit() => _TurnOffBool("hit");

        public void FlipSprite(bool right = true)
        {
            spr.flipX = right;
            if (addtionFlipSpr != null)
            {
                addtionFlipSpr.localScale = right ? new Vector3(-1, 1, 1) : Vector3.one;
            }
        }

        public void SetWeaponSpriteObject(Transform weapon)
        {
            this.addtionFlipSpr = weapon;
        }

        public void TakeDamage(float value)
        {
            if (anim != null)
            {
                anim.SetBool("hit", true);
            }
            if (stats.Shield > value)
            {
                stats.Shield -= value;
            }
            else if (stats.Shield > 0)
            {
                float damageLeft = value - stats.Shield;
                stats.Shield = 0;
                stats.Health -= damageLeft;
                if (shieldBreak != null)
                {
                    shieldBreak.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    shieldBreak.Play(true);
                }
			}
            else
            {
                stats.Health -= value;
            }
            Invoke("_StopHit", 0.02f);
        }

        public void ReceiveExp(float value = 1) => playerLeveling.ReceiveExp(value);

        public void HealShield(float value) => stats.Shield += value;
        public void Heal(float value) => stats.Health += value;

        /// <summary>
        /// Heal part of the missing health
        /// </summary>
        /// <param name="percentage">0.3 if it's 30%</param>
		public void HealMissingHp(float percentage) => stats.Health += (100 - stats.Health) * percentage;

        public class PlayerLeveling
        {
            private int level;
            private float currentExp;
            private float expUntilLevelUp => 8 * Mathf.Pow(level, 0.5f);

            public int Level => level;
            public float CurrentExp => currentExp;
            public float ExpProgress => currentExp / expUntilLevelUp;
            private Action<int> OnLevelUp { get; set; }

            public PlayerLeveling(Action<int> OnLevelUp)
            {
                level = 1;
                currentExp = 0;
                this.OnLevelUp = OnLevelUp;
            }
            public PlayerLeveling() : this((i) => { }) { }

            public void ReceiveExp(float value)
            {
                //  Debug.Log("tang xp" + value);
                currentExp += value;
                while (currentExp > expUntilLevelUp)
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
            private float health = 100f;
            public float Health
            {
                get => health;
                set
                {
                    health = Mathf.Clamp(value, 0, 100);
                }
            }
            private float shield = 100f;
            public float Shield
            {
                get => shield;
                set
                {
                    shield = Mathf.Clamp(value, 0, 100);
                }
            }
            public float baseAtk = 1f;
            public float atkMultipler = 1f;
            public float baseSpeed = 1f;
            public float speedMultipler = 1f;
        }
    }
}