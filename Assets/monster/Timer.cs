using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monster
    {
    public class Timer : MonoBehaviour
    {

        float totalsecond;
        float elapsedsecond;
        bool running = false;
        bool started = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (running)
            {
                elapsedsecond += Time.deltaTime; //>2
                if (elapsedsecond > totalsecond) //2
                {
                    running = false;
                }
            }
        }

        public void run()
        {
            if (totalsecond > 0)
            {
                started = true;
                running = true;
                elapsedsecond = 0;
            }
        }

        public float duration //2
        {
            set
            {
                if (!running)
                {
                    totalsecond = value; //2
                }
            }
        }

        public bool finished //false
        {
            get { return started && !running; }
        }

        public bool Running //false
        {
            get { return running; }
        }
    }
}