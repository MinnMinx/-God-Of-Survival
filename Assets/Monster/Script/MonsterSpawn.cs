using Core;
using Monster;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterSpawn : MonoBehaviour
    {
        [SerializeField]
        private Core.PlayerController player;

        [SerializeField]
        List<GameObject> monsters;

        float screenLeft;
        float screenRight;
        float screenTop;
        float screenBottom;

        private float spawnrate;
        private int level;
        private float heso = 0.3f;
        private float time = 0;
        public static List<GameObject> spawned = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            player.OnLevelUp_Addition += SpecialLevelUpSpawn;
		}

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            saveScreenSize();
            SetSpwanrate();
            //Debug.Log("" + spawnrate);
            if (time >= spawnrate || spawned.Count == 0)
            {
                System.Random rnd = new System.Random();
                List<float> x = new List<float>();
                x.Add(UnityEngine.Random.Range(screenLeft - 1, screenLeft));
                x.Add(UnityEngine.Random.Range(screenRight, screenLeft + 1));

                List<float> y = new List<float>();
                y.Add(UnityEngine.Random.Range(screenBottom - 1, screenBottom));
                y.Add(UnityEngine.Random.Range(screenTop, screenTop + 1));

                Vector3 pos = new Vector3(x[rnd.Next(x.Count)], y[rnd.Next(y.Count)], 0);
                spawn(pos);
                time = 0;
            }
        }


        void SpecialLevelUpSpawn(int level)
        {
            if (level % 5 == 0)
            {
                spawn(Camera.main.RandomPointOnEdgeScreen(), true);
			}
        }

        public void spawn(Vector3 position, bool isSpecial = false)
        {
            System.Random rnd = new System.Random();
            GameObject monster = Instantiate(monsters[rnd.Next(monsters.Count)]);
            monster.transform.position = position;
            monster.GetComponent<Monster>().SetPlayer(player, isSpecial);
            spawned.Add(monster);
        }


        public void SetSpwanrate()
        {
            level = player.Level;
            float a = (float)Math.Pow(level, heso);
            spawnrate = player.Level < 15 ? 3 / a : player.Level < 30 ? 2 / a : 1 / a;
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