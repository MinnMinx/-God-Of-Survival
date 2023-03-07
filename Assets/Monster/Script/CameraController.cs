using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform transforme;
        // Start is called before the first frame update
        void Start()
        {
            if (transforme == null)
                transforme = GameObject.Find("Circle").transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 v = new Vector3(transforme.position.x, transforme.position.y, transform.position.z);
            transform.position = v;
        }
    }
}
