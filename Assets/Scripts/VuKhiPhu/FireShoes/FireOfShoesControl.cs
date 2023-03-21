using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuKhi;

public class FireOfShoesControl : Base
{
    [SerializeField]
    public GameObject fireshoes;

    private Transform player;
    private float time = 0;
    private float cd = 0.2f;
    public float liftime = 2f;
    List<fireshoes2> list = new List<fireshoes2>();
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        base.ATKBase = 3f;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var item in list)
        {
            item.time2 += Time.deltaTime;
            if (item.time2 >= liftime)
            {
                Destroy(item.fire);
                list.Remove(item);
            }
        }

        time += Time.deltaTime;
        if (time > cd)
        {
            Debug.Log("" + liftime);
            spawnfire();
            time = 0;
        }
    }

    void spawnfire()
    {
        fireshoes2 f = new fireshoes2();
        f.fire = Instantiate(fireshoes, player.position, Quaternion.identity);
        f.lifetime = this.liftime;
        f.time2 = 0;
        list.Add(f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger fire");
        // Destroy(gameObject); 

        var enemy = other.gameObject.GetComponent<Monster.Monster>();
        if (enemy != null)
        {
            Debug.Log("ouch fire");
            enemy.takedamage(ATKBase);
        }
    }

    public class fireshoes2
    {
        public GameObject fire { get;  set; }
        public float lifetime { get;  set; }
        public float time2 { get; set; }
    }
}
