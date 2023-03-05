using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace VuKhi
{
    public class SwordController : Base
    {
       

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame

        void Update()
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                //Tấn công 1 lần (Di chuyển 3/4 hình tròn)

                time = 1;
                Debug.Log("Đã bị tấn công" + ATKBase);
            }

        }
        //khi đối tượng chạm vào quái 
        void OnCollisionEnter2D(Collision2D col)
        {


        }

        void Attak()
        {

        }
    }
}

