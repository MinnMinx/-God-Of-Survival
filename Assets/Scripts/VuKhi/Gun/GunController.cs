using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VuKhi
{
    public class GunController : Base
    {
        // Start is called before the first frame update
        public GameObject bulletPrefab;
        public Transform bulletSpawn;

        public void Shoot()
        {
            //"bulletPrefab" là prefab cho viên đạn
            // "bulletSpawn" là vị trí khởi đầu của viên đạn.
            // Khi phương thức Shoot được gọi, chúng ta sẽ tạo một instance của prefab đạn tại vị trí và hướng của "bulletSpawn".
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            //muốn bắn đạn khi người chơi ấn phím "Space", bạn có thể thêm đoạn mã sau vào script của nhân vật
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    GetComponent<GunController>().Shoot();
            //}
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                // xử lý khi viên đạn chạm vào đối tượng Enemy
                Destroy(other.gameObject); // hủy đối tượng Enemy
                Destroy(gameObject); // hủy đối tượng viên đạn
            }
        }
        void Start()
        {

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
