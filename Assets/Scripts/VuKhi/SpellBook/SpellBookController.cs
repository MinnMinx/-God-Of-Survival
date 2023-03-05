using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VuKhi
{
    public class SpellBookController : Base
    {
        GameObject attackArea = new GameObject("AttackArea");
       // attackArea.AddComponent<PolygonCollider2D>();
         float attackRange = 5f;
        // Start is called before the first frame update
        void Start()
        {
            Vector3[] vertices = new Vector3[3];
            vertices[0] = transform.position; // đỉnh tam giác là vị trí của nhân vật
            vertices[1] = vertices[0] + (Quaternion.Euler(0, 0, 45) * transform.right) * attackRange; // đỉnh 1 của tam giác
            vertices[2] = vertices[0] + (Quaternion.Euler(0, 0, -45) * transform.right) * attackRange; // đỉnh 2 của tam giác

            //attackArea.GetComponent<PolygonCollider2D>().SetPath(0, vertices);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                // gây sát thương cho quái vật
                //other.gameObject.GetComponent<Enemy>().TakeDamage(ATKBase);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
