using Mono.Cecil;
using Monster;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using VuKhi;

namespace VuKhiPhu
{
    public class MiniBombController : Base
    {
        public GameObject bombPrefab;
        public float spawnTime = 5f;
        //public float explosionRadius = 5f; // Bán kính của vùng nổ
        //public float explosionForce = 1000f; // Lực nổ
        //public float explosionTime = 3f; // Thời gian phát nổ
        public GameObject explosionEffect; // Hiệu ứng nổ
        public GameObject bomcontrol;
        public static bool maxBomb = false;


        private void Start()
        {
            bomcontrol = GameObject.Find("BombController");
        }



        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<Core.PlayerController>();
            var minibom = bomcontrol.GetComponent<MiniBomb>();
            var firecheck = minibom.active;
            if (player != null && !firecheck)
            {
                minibom.active = true;
                minibom.count = 1;
                Destroy(gameObject);
            }
            else if (player != null && firecheck)
            {
                if (minibom.cd >= 1)
                {
                    minibom.ATKBase++;
                    minibom.cd -= 0.5f;
                    minibom.speed += 0.2f;
                    Destroy(gameObject);
                }
                else
                {
                    maxBomb = true;
                    player.Heal(20);
                    Destroy(gameObject);
                }
            }
        }
    }
}

