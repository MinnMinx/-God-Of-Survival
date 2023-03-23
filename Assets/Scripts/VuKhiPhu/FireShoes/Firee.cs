using Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firee : VuKhi.Base
{
    [SerializeField]
    private float hurtCd = 1f;
    private List<EnemyHurtTime> hurtData = new List<EnemyHurtTime>();
    // Start is called before the first frame update
    void Start()
    {
        ATKBase = 2;
    }
	private void Update()
	{
		for(int i = 0; i < hurtData.Count; i++)
        {
            if (hurtData[i] == null)
            {
                hurtData.RemoveAt(i--);
                continue;
            }
            if (Time.time > hurtData[i].hurtTime + hurtCd)
            {
                hurtData[i].enemy.takedamage(ATKBase);
                hurtData[i].hurtTime = Time.time;
            }
        }
	}

	void OnTriggerStay2D(Collider2D other)
    {
        // Destroy(gameObject); 
        var enemy = other.gameObject.GetComponent<Monster.Monster>();
        if (enemy != null)
        {
            for(int i = 0; i < hurtData.Count; i++)
            {
                if (enemy == hurtData[i].enemy)
                {
                    return;
                }
            }
            hurtData.Add(new EnemyHurtTime
            {
                enemy = enemy,
                hurtTime = Time.time,
            });
        }
    }

    class EnemyHurtTime
    {
        public Monster.Monster enemy;
        public float hurtTime;
    }
}
