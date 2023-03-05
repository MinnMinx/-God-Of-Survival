using Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monsterõ
{
    public class monsterspawn : MonoBehaviour
    {
        [SerializeField]
        GameObject monster;

        Timer timer;

        float screenLeft;
        float screenRight;
        float screenTop;
        float screenBottom;

        // Start is called before the first frame update
        void Start()
        {
            saveScreenSize();
            timer = gameObject.AddComponent<Timer>();
            timer.duration = 2f;
            timer.run();
        }

        // Update is called once per frame
        void Update()
        {
            if (timer.finished)
            {
                Vector3 pos = new Vector3(UnityEngine.Random.Range(screenLeft - 10, screenRight + 10), UnityEngine.Random.Range(screenBottom - 10, screenTop + 10), -Camera.main.transform.position.z);
                spawn(pos);
                timer.run();
            }
        }



        public void spawn(Vector3 position)
        {
            GameObject ball = Instantiate(monster);
            ball.transform.position = position;

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