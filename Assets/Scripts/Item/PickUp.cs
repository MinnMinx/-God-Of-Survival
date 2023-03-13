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
			this.Invoke("_Destroy", countDownUntilDespawn);
		}
		private void OnBecameVisible()
		{
			this.CancelInvoke("_Destroy");
		}
		private void _Destroy()
		{
			OnDespawn();
			Destroy(gameObject);
		}
	}
}

