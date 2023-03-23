using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using VuKhi;
using UnityEngine.UIElements;
using Transform = UnityEngine.Transform;
using System;
using System.Threading;
using Item;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using Unity.Mathematics;

namespace VuKhiPhu
{
    public class FireBallController : Base
    {
        private Transform player;
        private static float speed;
        private static int count = 0;
        private static float orbitDistance = 3.0f;
        public static bool maxFire = false;
        private int index = -1;

		void Update()
        {
            if (index > 0 && player != null)
                Orbit();
        }

        void Orbit()
        {
			float angle = 360f / count * index + Time.time * speed * 20f;
			transform.position = new Vector3(
					Mathf.Cos(angle * Mathf.Deg2Rad),
					Mathf.Sin(angle * Mathf.Deg2Rad)
				) * orbitDistance
				+ player.position;
        }

        // quả cầu lửa nào chạm vào kẻ địch và gây sát thương 
        void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.gameObject.GetComponent<Monster.Monster>();
            if (enemy != null && index >= 0)
            {
                enemy.takedamage(ATKBase);
                return;
            }

            var player = other.gameObject.GetComponent<Core.PlayerController>();
            if (player != null && index <= 0)
            {
				this.player = other.transform;
				if (count < 10)
				{
					index = ++count;
				}
				LevelUp();
			}
        }

        void LevelUp()
        {
			switch (count)
			{
				case 1:
					orbitDistance = 4f;
					speed = 6f;
					break;
				case 2:
					orbitDistance = 4.2f;
					speed = 10f;
					break;
				case 3:
					orbitDistance = 4.6f;
					speed = 12f;
					break;
				case 4:
					orbitDistance = 5.4f;
					speed = 14f;
					break;
				case 5:
					orbitDistance = 6.8f;
					speed = 16f;
					break;
			}
			if (count > 5)
			{
				player.GetComponent<Core.PlayerController>().Heal(20);
				if (index < 0)
					Destroy(gameObject);
			}
		}
    }

}
