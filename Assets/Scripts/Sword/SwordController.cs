using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    int ATKBase = 3;
    int ATKSpeed = 1;
    float time = 0;

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
