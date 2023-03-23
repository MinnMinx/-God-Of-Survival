using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuKhi;

public class SwordCollisionBehavior : MonoBehaviour
{
	[SerializeField]
	private SwordController controller;

	private void OnParticleCollision(GameObject other)
	{
		Monster.Monster m = other.GetComponent<Monster.Monster>();
		if (m != null)
		{
			m.takedamage(controller.Damage);
		}
	}
}
