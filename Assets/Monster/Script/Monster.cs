using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCtrl = Core.PlayerController;

namespace Monster
{
    public class Monster : MonoBehaviour
    {
        Transform destination; // player's transfrom

        [SerializeField]
        private float speed;
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

        private float heso = 0.5F;

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


        public void Start()
        {
            saveScreenSize();
        }

        public void SetPlayer(PlayerCtrl player)
        {
            destination = player.transform;
            level = player.Level;
        }

        // Update được gọi mỗi frame
        public void Update()
        {
            float step = speed * Time.deltaTime; // Tính toán khoảng cách Object di chuyển trong mỗi frame
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step); // Di chuyển Object đến vị trí đích

            var distance = Vector3.Distance(transform.position, destination.position);

            cd += Time.deltaTime;
            if (distance <= atkrange && cd >= atkspeed)
            {
                Attack();
            }

            if (distance - (screenRight - screenLeft) * 1.5 >= 0) this.Despawn();
        }

        public float GetHp(float basehp)
        {
            float a = (float)Math.Pow(level, heso);
            hp = basehp * a;
            return hp;
        }

        public void Attack()
        {
            Debug.Log("Attack");
            cd = 0;
        }

        public void takedamage(float dame)
        {
            hp = hp - dame;
        }

        void Despawn()
        {
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
    }
}