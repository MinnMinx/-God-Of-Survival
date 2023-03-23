using Monster;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;
using VuKhi;

public class MiniBomb : Base
{
    public bool active = false;
    [SerializeField]
    public GameObject bomb;

    [SerializeField]
    public GameObject boom;
    private float time = 5;
    private float lifetime = 2f;
    public int count = 0;
    public float speed = 10f;
    public float cd = 5f;

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
        for (int i = 0; i < boms.Count; i++)
        {
            var item = boms[i];
            item.timebom += Time.deltaTime;
            if (item.timebom >= lifetime && item.bomprefab != null)
            {
                Explosion(item.bomprefab.transform.position);
                Destroy(item.bomprefab);
                boms.RemoveAt(i);
                i--;
            }
        }


        monsters = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        monsters = monsters.Where(x => Vector3.Distance(player.position, x.transform.position) <= 10f).ToList();
        if (time > cd && count > 0 && monsters.Count > 0)
        {
            SpawnBomb(monsters.ElementAtOrDefault(0).transform.position);
            time = 0f;
        }
    }

    //Tạo bom
    void SpawnBomb(Vector3 v3)
    {
        var bommms = new boombb();
        Vector2 target = new Vector2(v3.x, v3.y);
        Vector2 direction = (target - (Vector2)player.position).normalized;
        bommms.bomprefab = Instantiate(bomb, player.position, Quaternion.Euler(0, 0, -Vector2.SignedAngle(direction, Vector2.up)));
        var rb = bommms.bomprefab.GetComponent<Rigidbody2D>();
        //newBullet.GetComponent<BulletController>().Atk = Atk;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);

        bommms.speed = this.speed;
        boms.Add(bommms);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var monster = other.gameObject.GetComponent<Monster.Monster>();
        if (monster != null && active)
        {
            Explosion(other.gameObject.transform.position);
        }
    }

    void Explosion(Vector3 v)
    {
      var a =  Instantiate(boom, v, Quaternion.identity);
    }

    public class boombb
    {
        public GameObject bomprefab { get; set; }
        public float speed { get; set; }
        public float timebom { get; set; } = 0f;
    }
}
