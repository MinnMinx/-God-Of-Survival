using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firee : VuKhi.Base
{
    // Start is called before the first frame update
    void Start()
    {
        ATKBase = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Destroy(gameObject); 
        var enemy = other.gameObject.GetComponent<Monster.Monster>();
        if (enemy != null)
        {
            Debug.Log("ouch fire");
            enemy.takedamage(ATKBase);
        }
    }
}
