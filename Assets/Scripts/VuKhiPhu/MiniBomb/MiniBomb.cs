using Monster;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;
using VuKhi;

public class MiniBomb : Base
{
    public static bool active = false;
    [SerializeField]
    public GameObject bomb;

    [SerializeField]
    public GameObject boom;
    private float time = 5;
    private float lifetime = 1f;
    public int count = 0;
    public float speed = 5f;
    private float cd = 5f;

    private List<GameObject> monsters = new List<GameObject>();
    List<boombb> boms = new List<boombb>();
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        ATKBase = 5;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        time += Time.deltaTime;
        foreach (var item in boms)
        {
            item.timebom += Time.deltaTime;
            if (item.timebom >= lifetime)
            {
                Explosion(item.bomprefab.transform.position);
            }
        }


        monsters = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        monsters = monsters.Where(x => Vector3.Distance(player.position, x.transform.position) <= 10f).ToList();
        Debug.Log("count: " + count + " monster: " + monsters.Count);
        if (time >= cd && count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnBomb(monsters.ElementAt(i).transform.position);
            }
            time = 0f;
        }
    }

    //Tạo bom
    void SpawnBomb(Vector3 v3)
    {
        var bommms = new boombb();
        bommms.bomprefab = Instantiate(this.bomb, player.position, Quaternion.identity);
        bommms.speed = this.speed;
        bommms.bomprefab.transform.position = Vector3.MoveTowards(player.position, v3, speed * Time.deltaTime);
        boms.Add(bommms);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var monster = other.gameObject.GetComponent<Monster.Monster>();
        if (monster != null && active)
        {
            monster.takedamage(ATKBase);
        }
    }

    void Explosion(Vector3 v)
    {
        //Instantiate(boom, v, Quaternion.identity);
    }

    public class boombb
    {
        public GameObject bomprefab { get; set; }
        public float speed { get; set; }
        public float timebom { get; set; } = 0f;
    }
}
