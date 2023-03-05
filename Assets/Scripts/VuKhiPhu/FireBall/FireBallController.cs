using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using VuKhi;
using UnityEngine.UIElements;

namespace VuKhiPhu
{
    public class FireBallController : Base
    {
        public GameObject fireballprefab;



        // Start is called before the first frame update
        void Start()
        {
            //Thiết lập vị trí và định hướng ban đầu cho quả cầu lửa
            //fireball.transform.position = transform.position + (Quaternion.Euler(0, 0, angle) * Vector3.right) * 10f; // vị trí ban đầu của quả cầu lửa
            //fireball.transform.rotation = Quaternion.Euler(0, 0, angle + 90); // định hướng ban đầu của quả cầu lửa
        }

        // Update is called once per frame
        void Update()
        {
            //Lặp lại việc tạo và thiết lập các quả cầu lửa với các góc khác nhau để tạo ra hiệu ứng quay quanh player
            //for (int i = 0; i < numFireballs; i++)
            //{
            //    float angle = i * 360f / numFireballs; // góc giữa các quả cầu lửa
            //    GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            //    fireball.transform.position = transform.position + (Quaternion.Euler(0, 0, angle) * Vector3.right) * 10f;
            //    fireball.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            //}
        }
        // quả cầu lửa nào chạm vào kẻ địch và gây sát thương 
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                //other.GetComponent<Enemy>().TakeDamage(ATKBase); // gây sát thương
                Destroy(gameObject); // phá hủy quả cầu lửa
            }
        }
        //phương thức để sử dụng phép thuật của nhân vật, sử dụng một vòng lặp để sinh ra các quả cầu lửa.
        //void CastSpell()
        //{
        //    for (int i = 0; i < numFireballs; i++)
        //    {
        //        float angle = i * Mathf.PI * 2 / numFireballs; // tính góc giữa các quả cầu lửa
        //        Vector3 pos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius; // tính vị trí của quả cầu lửa trên đường tròn

        //        GameObject fireball = Instantiate(fireballPrefab, pos, Quaternion.identity); // sinh ra quả cầu lửa
        //        fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed; // cho quả cầu lửa di chuyển theo hướng của nó

        //        // set damage tương ứng cho quả cầu lửa
        //        fireball.GetComponent<Fireball>().damage = ATK;
        //    }
        //}
    }

}
