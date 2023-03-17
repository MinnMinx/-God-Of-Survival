using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = 25f * transform.right; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("ouch");

            Destroy(gameObject); // hủy đối tượng viên đạn

            var enemy = other.gameObject.GetComponent<Monster.Monster>();
            if(enemy != null)
            {
                enemy.takedamage(1);
            }

        }
    }
}
