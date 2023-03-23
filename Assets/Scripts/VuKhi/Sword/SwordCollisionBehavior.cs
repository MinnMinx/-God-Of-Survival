using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuKhi;

public class SwordCollisionBehavior : MonoBehaviour
{
	[SerializeField]
	private SwordController controller;

	[SerializeField]
	private List<GameObject> damagedObj = new List<GameObject>(20);

	private void OnParticleCollision(GameObject other)
	{
		Monster.Monster m = other.GetComponent<Monster.Monster>();
		if (m != null && !damagedObj.Contains(other))
		{
			m.takedamage(controller.Damage);
			damagedObj.Add(other);
		}
	}

	public void Clear()
	{
		damagedObj.Clear();
	}
}
