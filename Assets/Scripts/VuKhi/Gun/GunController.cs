using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VuKhi
{
    public class GunController : Base
    {
        // Start is called before the first frame update
        
        public GameObject bulletPrefab;

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



        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(12);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("ouch");

                // xử lý khi viên đạn chạm vào đối tượng Enemy
                Destroy(other.gameObject); // hủy đối tượng Enemy
                Destroy(gameObject); // hủy đối tượng viên đạn
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        
    }

}
