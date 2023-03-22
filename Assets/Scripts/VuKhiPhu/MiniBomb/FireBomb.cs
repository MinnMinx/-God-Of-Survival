using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireBomb : MonoBehaviour
{
    [SerializeField]
    private GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var monster = other.gameObject.GetComponent<Monster.Monster>();
        if (monster != null)
        {
            Explosion(other.gameObject.transform.position);
            Destroy(gameObject);
        }
    }

    void Explosion(Vector3 v)
    {
        var a = Instantiate(boom, v, Quaternion.identity);
    }
}
