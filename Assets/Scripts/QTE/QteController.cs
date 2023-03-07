using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QTE {
    public class QteController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] qtePrefabs;

        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private Vector2 spawningCdRange = Vector2.one * 10f;
        private float spawningCdTime = 0;
        private IQte currentQte;

        private void Start()
        {
            ResetCd();
        }

        // Update is called once per frame
        void Update()
        {
            float deltaTime = Time.deltaTime;
            if (spawningCdTime > 0)
                spawningCdTime -= deltaTime;

            // if there's no alive QTE
            if (spawningCdTime <= 0 && (currentQte == null || currentQte.IsOver))
            {
                if (currentQte != null)
                {
                    // Cleanup
                    currentQte.CleanUp();
                    Destroy((currentQte as MonoBehaviour).gameObject);
                }

                var gameObj = Instantiate(qtePrefabs[Random.Range(0, qtePrefabs.Length)]);
                if ((currentQte = gameObj.GetComponent(typeof(IQte)) as IQte) == null)
                {
                    Debug.Log("Cannot convert this way");
                    Destroy(gameObj);
                    return;
                }
                currentQte.OnStart();
                ResetCd();
            }
            else if (currentQte != null && !currentQte.IsOver)
            {
                currentQte.OnUpdate(deltaTime);
            }
        }

        void ResetCd()
        {
            spawningCdTime = Random.Range(spawningCdRange.x, spawningCdRange.y);
            Debug.Log(spawningCdTime);
        }
    }
}
