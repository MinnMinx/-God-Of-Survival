using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuKhi;

public class SpellbookScript : Base
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("get weapon xp");
        var player = collision.gameObject.GetComponent<Core.PlayerController>();
        if (player != null)
        {
            Debug.Log("get weapon xp");
        }
    }
}
