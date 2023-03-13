using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class Player : MonoBehaviour
    {
        private float hp = 100;
        private float xp;

        [SerializeField]
        private float speed = 5;
        public float Speed
        {
            get { return speed; }
            set { speed= value; }
        }

        private float atk;
        public float Atk
        {
            get { return atk; }
            set { atk = value; }
        }
        
        private int level = 5;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        [SerializeField]
        private MapController mapCtrl; // Will move this into input controller in the future

        private SpriteRenderer sprite;

        // Start is called before the first frame update
        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
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
                if (horizontal >= 0) sprite.flipX= false;
                else sprite.flipX= true;
            }

            if (vertical != 0)
            {
                v.y += vertical * speed * Time.deltaTime;
            }
            // move
            transform.position = v;
            if (mapCtrl != null)
            {
                //mapCtrl.SetPosition(v);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            takedamage(collision.gameObject.GetComponent<Monster>().Atk);
            Debug.Log("HP: " + hp);
        }

        void takedamage(float dame)
        {
            hp = hp - dame;
        }
    }
}