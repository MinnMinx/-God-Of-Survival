using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item {
    public class PickUpController : MonoBehaviour
    {
        private Collider2D[] pickableItems = new Collider2D[10]; // max 10 item per frame
        private List<PickUp> pickingUpItems = new List<PickUp>(30);
        [SerializeField]
        private float pickUpRadius = 3f;
		[SerializeField]
		private float pickUpFlySpeed = 2f;

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
            float deltaTime = Time.deltaTime;
			if (Physics2D.OverlapCircleNonAlloc(contextObjects.player.transform.position, pickUpRadius, pickableItems, itemLayerMask) > 0)
            {
                foreach (var collider in pickableItems)
                {
                    PickUp item;
					if (collider != null && (item = collider.GetComponent<PickUp>()) != null)
                    {
                        collider.enabled = false;
                        pickingUpItems.Add(item);
					}
                }
            }
            for (int i = 0; i < pickingUpItems.Count; i++)
            {
                var pickingItem = pickingUpItems[i];
                pickingItem.transform.position = Vector3.MoveTowards(pickingItem.transform.position, contextObjects.player.transform.position, deltaTime * pickUpFlySpeed);
                if (Vector3.Distance(pickingItem.transform.position, contextObjects.player.transform.position) <= 0.3f)
                {
                    // Pick up
                    pickingItem.OnPickUp(contextObjects);
                    pickingItem.OnDespawn();
                    RemoveItemBySwap(i);
                    i--;
					Destroy(pickingItem.gameObject);
                }
            }
            spawner.Update(deltaTime);
		}

		private void OnDrawGizmosSelected()
		{
            if (contextObjects.player != null)
                Gizmos.DrawSphere(contextObjects.player.transform.position, pickUpRadius);
		}

        void RemoveItemBySwap(int index)
        {
			pickingUpItems[index] = pickingUpItems[pickingUpItems.Count - 1];
			pickingUpItems.RemoveAt(pickingUpItems.Count - 1);
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
