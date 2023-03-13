using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public abstract class PickUp : MonoBehaviour
    {
		private float countDownUntilDespawn = 10;
		private void Start()
		{
			OnSpawn();
		}

		public virtual void OnSpawn() { }
		public virtual void OnDespawn() { }
        public abstract void OnPickUp (PickUpController.PickUpContext context);

		private void OnBecameInvisible()
		{
			countDownUntilDespawn -= Time.deltaTime;
			if (countDownUntilDespawn <= 0)
			{
				OnDespawn();
				Destroy(gameObject);
			}
		}
		private void OnBecameVisible()
		{
			countDownUntilDespawn = 10;
		}
	}
}

