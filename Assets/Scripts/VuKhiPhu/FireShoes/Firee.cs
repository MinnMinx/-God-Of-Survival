using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firee : VuKhi.Base
{
    private float cd = 1;
    // Start is called before the first frame update
    void Start()
    {
        ATKBase = 5;
    }

    // Update is called once per frame
    void Update()
    {
        cd += Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Destroy(gameObject); 
        var enemy = other.gameObject.GetComponent<Monster.Monster>();
        if (enemy != null && cd >= 1)
        {
            Debug.Log("ouch fire");
            enemy.takedamage(ATKBase);
            cd = 0;
        }
    }
}
