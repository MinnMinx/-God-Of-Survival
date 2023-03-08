using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuKhi;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //muốn bắn đạn khi người chơi ấn phím "Space", bạn có thể thêm đoạn mã sau vào script của nhân vật
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        }
    }
}
