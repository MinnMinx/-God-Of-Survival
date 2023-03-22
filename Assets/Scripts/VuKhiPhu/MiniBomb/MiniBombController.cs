using Monster;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VuKhi;

namespace VuKhiPhu
{
    public class MiniBombController : Base
    {
        public GameObject bombPrefab;
        public GameObject player;
        public float spawnTime = 5f;

        private float timer = 0f;

        public float explosionRadius = 5f; // Bán kính của vùng nổ
        public float explosionForce = 1000f; // Lực nổ
        public float explosionTime = 3f; // Thời gian phát nổ
        public GameObject explosionEffect; // Hiệu ứng nổ

        private bool isExploded = false; // Kiểm tra xem quả bom đã phát nổ chưa

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        ////sinh ra vị trí ném bom:
        Vector3 GetThrowPosition(Vector3 playerPosition, GameObject enemy, float radius)
        {
            Vector3 throwPosition;

            if (enemy != null)
            {
                throwPosition = enemy.transform.position;
            }
            else
            {
                float randomAngle = Random.Range(0f, Mathf.PI * 2f);
                float randomRadius = Random.Range(0f, radius);
                throwPosition = new Vector3(playerPosition.x + randomRadius * Mathf.Cos(randomAngle), playerPosition.y, playerPosition.z + randomRadius * Mathf.Sin(randomAngle));
            }

            return throwPosition;
        }
        ////Hàm để tính khoảng cách giữa player và địch
        float Distance(Vector3 playerPosition, Vector3 enemyPosition)
        {
            return Vector3.Distance(playerPosition, enemyPosition);
        }
        ////tìm địch gần nhất trong bán kính 30f:
        GameObject FindClosestEnemy(Vector3 playerPosition, float radius)
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float distance = Distance(playerPosition, enemy.transform.position);
                if (distance < closestDistance && distance <= radius)
                {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }
            }

            return closestEnemy;
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= spawnTime)
            {
                SpawnBomb();
                timer = 0f;
            }
        }
        //Tạo bom
        void SpawnBomb()
        {
            Instantiate(bombPrefab, GetThrowPosition(player.transform.position, FindClosestEnemy(player.transform.position, 10f), 10f), Quaternion.identity);
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isExploded && collision.gameObject.CompareTag("Enemy"))
            {
                isExploded = true;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

                foreach (Collider2D nearbyObject in colliders)
                {
                    //Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
                    //if (rb != null)
                    //{
                    //    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    //}

                    Monster.Monster enemy = nearbyObject.GetComponent<Monster.Monster>();
                    if (enemy != null)
                    {
                        Debug.Log("minibom");
                        enemy.takedamage(10); // Gây sát thương cho quái
                    }
                }

                // Tạo hiệu ứng nổ
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

                // Hủy quả bom
                Destroy(gameObject, explosionTime);
            }
        }
    }

}

