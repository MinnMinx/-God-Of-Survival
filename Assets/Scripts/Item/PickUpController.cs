using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item {
    public class PickUpController : MonoBehaviour
    {
        private Collider2D[] pickableItems = new Collider2D[10]; // max 10 item per frame
        [SerializeField]
        private float pickUpRadius = 3f;

		[SerializeField]
		private PickUpContext contextObjects;
        [SerializeField]
        private PickUpSpawner spawner;
		private LayerMask itemLayerMask;

		private void Start()
		{
            itemLayerMask = LayerMask.GetMask("Item");
            if (spawner == null)
                spawner = new PickUpSpawner();
            spawner.Reset();
		}

		private void Update()
		{
			if (Physics2D.OverlapCircleNonAlloc(contextObjects.player.transform.position, pickUpRadius, pickableItems, itemLayerMask) > 0)
            {
                foreach (var collider in pickableItems)
                {
                    if (collider == null)
                        continue;

					PickUp item = collider.GetComponent<PickUp>();
                    if (item != null)
                    {
						collider.enabled = false;
						item.OnPickUp(contextObjects);
                        item.OnDespawn();
                        Destroy(item.gameObject);
                    }
                }
            }
            spawner.Update(Time.deltaTime);
		}

		private void OnDrawGizmosSelected()
		{
            if (contextObjects.player != null)
                Gizmos.DrawSphere(contextObjects.player.transform.position, pickUpRadius);
		}

		[System.Serializable]
        public class PickUpContext
        {
            public Core.PlayerController player;
            
        }
		[System.Serializable]
		public class PickUpSpawner
        {
            [SerializeField]
            private GameObject[] prefabs;
            [SerializeField]
            private Vector2 spawnCdRandomRange;
			[SerializeField]
			private Transform parent;
			private float spawnCd;

            public void Reset()
            {
                spawnCd = Random.Range(spawnCdRandomRange.x, spawnCdRandomRange.y);
            }

            public void Update(float deltaTime)
            {
                spawnCd -= deltaTime;
                if (spawnCd <= 0)
                {
                    var spawned = Instantiate(prefabs[Random.Range(0, prefabs.Length)], parent);
                    var position = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
                    position.z = 0;
                    spawned.transform.position = position;
                    Reset();

				}
			}
        }
	}
}
