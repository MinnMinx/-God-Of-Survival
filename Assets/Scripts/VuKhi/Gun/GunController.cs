using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VuKhi
{
    public class GunController : Base
    {
        // Start is called before the first frame update
        
        public GameObject bulletPrefab;
        public float fireRate = 0.5f; // tốc độ bắn đạn
        public float bulletSpeed = 10f; // tốc độ của đạn
        private float lastFireTime;
        public void Shoot()
        {
            //"bulletPrefab" là prefab cho viên đạn
            // "bulletSpawn" là vị trí khởi đầu của viên đạn.
            // Khi phương thức Shoot được gọi, chúng ta sẽ tạo một instance của prefab đạn tại vị trí và hướng của "bulletSpawn".
            Instantiate(bulletPrefab, transform.position, transform.rotation);

           
        }

      
        void Start()
        {

        }
       
        // Update is called once per frame
        void Update()
        {
            if (Time.time - lastFireTime > fireRate)
            {
                Shoot();
                lastFireTime = Time.time;
            }

            
        }
        
    }

}
