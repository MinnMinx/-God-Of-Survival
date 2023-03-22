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
        public Transform player;
        public float spawnTime = 5f;
        public static int Count = 0;
        private bool check = false;
        //public float explosionRadius = 5f; // Bán kính của vùng nổ
        //public float explosionForce = 1000f; // Lực nổ
        //public float explosionTime = 3f; // Thời gian phát nổ
        public GameObject explosionEffect; // Hiệu ứng nổ


        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }



        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<Core.PlayerController>();
            var firecheck = other.gameObject.GetComponent<MiniBomb>();
            if (player != null && firecheck == null)
            {
                check = true;
                other.gameObject.AddComponent<MiniBomb>();
                other.gameObject.GetComponent<MiniBomb>().bomb = this.bombPrefab;
                other.gameObject.GetComponent<MiniBomb>().boom = this.explosionEffect;
                Destroy(gameObject);
            }
            else if (player != null && firecheck != null)
            {
                if (firecheck.count < 3)
                {
                    firecheck.count ++;
                    firecheck.speed += 0.2f;
                    Destroy(gameObject);
                }
            }
        }
    }
}

