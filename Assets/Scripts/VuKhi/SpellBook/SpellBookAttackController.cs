using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuKhi
{
	[RequireComponent(typeof(ParticleSystem))]
	public class SpellBookAttackController : MonoBehaviour
	{
		[SerializeField]
		private SpellBookController _parent;
		private ParticleSystem ps;
		private List<ParticleCollisionEvent> collidedParticles;

		private void Start()
		{
			ps = GetComponent<ParticleSystem>();
			collidedParticles = new List<ParticleCollisionEvent>(ps.main.maxParticles);
		}

		private void OnParticleCollision(GameObject other)
		{
			if (_parent == null)
				return;

			int numCollisionEvents = ps.GetCollisionEvents(other, collidedParticles);
			Monster.Monster monster = null;
			while (numCollisionEvents-- > 0)
			{
				monster = collidedParticles[numCollisionEvents].colliderComponent.GetComponent<Monster.Monster>();
				if (monster != null)
				{
					monster.takedamage(_parent.ATKBase);
					_parent.Lv3Behavior(monster);
					_parent.Lv5Behavior();
				}
			}
		}
	}
}