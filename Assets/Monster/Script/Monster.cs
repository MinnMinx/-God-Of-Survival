using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;
using VuKhiPhu;
using PlayerCtrl = Core.PlayerController;

namespace Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> listItem;
        [SerializeField]
        private List<GameObject> subWeapon;
        private Core.PlayerController player;
        private Animator anime;
        private bool checkflip = true;
        private bool specialSpawn = false;

        private Transform destination; // player's transfrom
        public Transform Des
        {
            get { return destination; }
            set { destination = value; }
        }

        [SerializeField]
        public float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private float atk;
        public float Atk
        {
            get { return atk; }
            set { atk = value; }
        }

        private float basehp;
        public float Basehp
        {
            get { return basehp; }
            set { basehp = value; }
        }

        private float hp;
        public float Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        private float heso = 0.9F;

        private float atkrange;
        public float Atkrange
        {
            get { return atkrange; }
            set { atkrange = value; }
        }

        private int level;

        private float atkspeed;
        public float Atkspeed
        {
            get { return atkspeed; }
            set { atkspeed = value; }
        }

        private float cd;
        public float Cd
        {
            get { return cd; }
            set { cd = value; }
        }

        float screenLeft;
        float screenRight;
        float screenTop;
        float screenBottom;

        private bool tinhanh;
        public bool Tinhanh
        {
            get { return tinhanh; }
            set { tinhanh = value; }
        }

        private float itemRate;
        public float ItemRate
        {
            get { return itemRate; }
            set { itemRate = value; }
        }
        public static Action<Vector3> SpecialDropAction = null;


        public void Start()
        {
            anime = GetComponent<Animator>();
            itemRate = 5f;
            tinhanhcheck();
            saveScreenSize();
        }

        public void SetPlayer(PlayerCtrl player, bool isSpecialSpawn = false)
        {
            this.player = player;
            Des = player.transform;
            level = player.Level;
			specialSpawn = isSpecialSpawn;
		}

        // Update được gọi mỗi frame
        public void Update()
        {
            float step = speed * Time.deltaTime; // Tính toán khoảng cách Object di chuyển trong mỗi frame
            if ((destination.position.x > transform.position.x && !checkflip) || (destination.position.x < transform.position.x && checkflip))
            {
                checkflip = !checkflip;
                Vector3 scale = transform.localScale;
                scale.x = -scale.x;
                transform.localScale = scale;
            }

            transform.position = Vector3.MoveTowards(transform.position, destination.position, step); // Di chuyển Object đến vị trí đích

            var distance = Vector3.Distance(transform.position, destination.position);

            cd += Time.deltaTime;
            if (distance < atkrange && cd >= atkspeed)
            {
                Attack();
            }

            if (distance - (screenRight - screenLeft) * 1.5 >= 0) this.Despawn();

            if (hp <= 0)
            {
                this.Despawn();
            }

            if (FireShoesController.maxShoes == true)
            {
                var a = subWeapon.Where(x => x.GetComponent<FireShoesController>() != null).FirstOrDefault();
                if (a != null)
                {
                    subWeapon.Remove(a);
                }
            }

            if (FireBallController.maxFire == true)
            {
                var a = subWeapon.Where(x => x.GetComponent<FireBallController>() != null).FirstOrDefault();
                if (a != null)
                {
                    subWeapon.Remove(a);
                }
            }

            if (MiniBombController.maxBomb == true)
            {
                var a = subWeapon.Where(x => x.GetComponent<MiniBombController>() != null).FirstOrDefault();
                if (a != null)
                {
                    subWeapon.Remove(a);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var a = collision.gameObject.GetComponent<Core.PlayerController>();
            if (a != null && cd > atkspeed)
            {
                a.TakeDamage(Atk);
                cd = 0;
            }
        }

        public float GetHp(float basehp)
        {
            float a = (float)Math.Pow(level, heso);
            hp = basehp * a;
            return hp;
        }

        public virtual void Attack()
        {
            cd = 0;
        }

        public void takedamage(float dame)
        {
            hp = hp - dame;
        }

        void Despawn()
        {
            int xp = tinhanh == true ? 2 : 1;
            player.ReceiveExp(xp);
            Drop();
            MonsterSpawn.spawned.Remove(gameObject);
            Destroy(this.gameObject);
        }

        private void saveScreenSize()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float screenZ = -Camera.main.transform.position.z;
            Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
            Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
            Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
            Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
            screenLeft = lowerLeftCornerWorld.x;
            screenRight = upperRightCornerWorld.x;
            screenTop = upperRightCornerWorld.y;
            screenBottom = lowerLeftCornerWorld.y;
        }

        private void tinhanhcheck()
        {
            if (specialSpawn)
            {
				tinhanh = true;
				return;
            }

            System.Random rnd = new System.Random();
            int check = rnd.Next(100);
            if (check < 5)
            {
                itemRate = 100;
                tinhanh = true;
            }
            else tinhanh = false;
        }

        private void Drop()
        {
            System.Random rnd = new System.Random();
            int check = rnd.Next(100);
            if (tinhanh && (specialSpawn || check <= 2) && SpecialDropAction != null)
            {
                SpecialDropAction(transform.position);
                return;
            }
            if (check <= itemRate && !tinhanh)
            {
                int check2 = rnd.Next(100);
                if (check2 < 70)
                {
                    int check3 = rnd.Next(listItem.Count);
                    Instantiate(listItem[check3], transform.position, Quaternion.identity);
                }
                else
                {
                    if (subWeapon.Count > 0)
                    {
                        
                        int check3 = rnd.Next(subWeapon.Count);
                        Instantiate(subWeapon[check3], transform.position, Quaternion.identity);
                    }
                }
            }
            else if (tinhanh)
            {
                if (subWeapon.Count > 0)
                {
                    int check3 = rnd.Next(subWeapon.Count);
                    Instantiate(subWeapon[check3], transform.position, Quaternion.identity);
                }
            }
        }
    }
}