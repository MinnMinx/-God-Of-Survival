using Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuKhi;

namespace VuKhiPhu
{
    public class MiniBombController : Base
    {
        GameObject bombPrefab;
        GameObject player;
        float explosionRadius = 5f;
        int throwTime = 1;
        private int enemyLayer;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        //Hàm để tạo ra quả bom
        void SpawnBomb(Vector3 throwPosition)
        {
            GameObject bomb = Instantiate(bombPrefab, player.transform.position, Quaternion.identity);
            //bomb.GetComponent<MiniBomb>().Throw(throwPosition, throwTime, explosionRadius, ATKBase);
        }
        // kiểm tra xem có kẻ địch nào trong phạm vi bán kính của quả bom
        bool CheckEnemyInRange(Vector3 center, float radius, out GameObject enemy)
        {
            Collider[] colliders = Physics.OverlapSphere(center, radius, enemyLayer);
            if (colliders.Length > 0)
            {
                // Chọn một kẻ địch ngẫu nhiên trong phạm vi bán kính
                enemy = colliders[Random.Range(0, colliders.Length)].gameObject;
                return true;
            }
            else
            {
                enemy = null;
                return false;
            }
        }
        //tạo ra một vị trí ngẫu nhiên trong phạm vi nếu không có kẻ địch nào trong phạm vi bán kính của quả bom:
        Vector3 GetRandomPosition(Vector3 center, float radius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Random.Range(0f, radius);
            Vector3 position = center + new Vector3(Mathf.Cos(angle) * distance, 0f, Mathf.Sin(angle) * distance);
            return position;
        }
        //sinh ra vị trí ném bom:
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

        //Hàm để tính khoảng cách giữa player và địch
        float Distance(Vector3 playerPosition, Vector3 enemyPosition)
        {
            return Vector3.Distance(playerPosition, enemyPosition);
        }

        //tìm địch gần nhất trong bán kính 30f:
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
        //In player:     khi player thực hiện hành động ném bom:
        //void ThrowBomb()
        //{
        //    Vector3 playerPosition = player.transform.position;
        //    GameObject enemy = FindClosestEnemy(playerPosition, bombRange);

        //    Vector3 throwPosition = GetThrowPosition(playerPosition, enemy, bombRange);
        //    SpawnBomb(throwPosition);
        //}
    }

}

