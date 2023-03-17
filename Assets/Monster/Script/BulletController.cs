using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class BulletController : MonoBehaviour
    {
        private float atk;
        public float Atk
        {
            get { return atk; }
            set { atk = value; }
        }
        private float time = 0;

        // Start is called before the first frame update
        private void Update() 
        {
            time += Time.deltaTime;
            if (time > 8f) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<Core.PlayerController>();
            if (player != null)
            {
                player.TakeDamage(atk);
                Destroy(this.gameObject);
            }
        }
    }
}

