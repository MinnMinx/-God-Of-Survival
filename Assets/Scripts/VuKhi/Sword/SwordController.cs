using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace VuKhi
{
    public class SwordController : Base
    {
        [SerializeField]
        private Transform parent;
		[SerializeField]
		private Core.PlayerController player;
		[SerializeField]
		private SwordFxController fx;
		float time;

        // Start is called before the first frame update
        void Start()
        {
            time = 0;
            player.SetWeaponSpriteObject(parent);
        }

        // Update is called once per frame

        void Update()
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                //Tấn công 1 lần (Di chuyển 3/4 hình tròn)

                time = 1 / ATKSpeed;
                fx.Swing();
            }

        }
        //khi đối tượng chạm vào quái 
        void OnCollisionEnter2D(Collision2D col)
        {


        }

        void Attak()
        {

        }
    }
}

