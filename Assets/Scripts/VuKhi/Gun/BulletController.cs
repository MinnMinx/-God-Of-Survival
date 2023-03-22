using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float damage;
    private bool goThrough;
    private List<GameObject> shooted;

    // Start is called before the first frame update
    public void Init(float damage, Vector2 velocityVector, bool goThrough)
    {
        this.damage = damage;
        this.goThrough = goThrough;
        GetComponent<Rigidbody2D>().velocity = velocityVector;
        if (goThrough)
            shooted = new List<GameObject>(10);
    }

	private void OnTriggerEnter2D(Collider2D other)
    {
        Monster.Monster enemy = other.gameObject.GetComponent<Monster.Monster>();
        if (enemy != null)
        {
            if (!goThrough) {
				enemy.takedamage(damage);
				Destroy(gameObject); // hủy đối tượng viên đạn
			}
            else if (!shooted.Contains(enemy.gameObject))
            {
                shooted.Add(gameObject);
				enemy.takedamage(damage);
			}
		}
    }

	private void OnBecameInvisible()
	{
        Destroy(gameObject);
	}
}
