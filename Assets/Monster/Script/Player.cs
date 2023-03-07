using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class Player : MonoBehaviour
    {
        float hp = 100;
        [SerializeField]
        float speed = 5;
        float atk;
        float xp;
        public int level = 5;

        [SerializeField]
        private MapController mapCtrl; // Will move this into input controller in the future

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 v = transform.position;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal != 0)
            {
                v.x += horizontal * speed * Time.deltaTime;
            }

            if (vertical != 0)
            {
                v.y += vertical * speed * Time.deltaTime;
            }
            // move
            transform.position = v;
            if (mapCtrl != null)
            {
                mapCtrl.SetPosition(v);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            takedamage(collision.gameObject.GetComponent<Monster>().atk);
            Debug.Log("HP: " + hp);
        }

        void takedamage(float dame)
        {
            hp = hp - dame;
        }
    }
}