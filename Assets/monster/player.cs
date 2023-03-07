using Assets.monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class player : MonoBehaviour
    {
        double hp = 100;
        float move = 5;

        public int level = 1;

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
                v.x += horizontal * move * Time.deltaTime;
            }

            if (vertical != 0)
            {
                v.y += vertical * move * Time.deltaTime;
            }
            // move
            transform.position = v;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("aaa" + hp);
        }
    }
}