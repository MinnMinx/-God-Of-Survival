using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QTE
{
    public class QteTimer
    {
        private float indicatorDuration = 0;
        private float timeSinceStart;

        public QteTimer(float indicatorDuration)
        {
            this.indicatorDuration = indicatorDuration;
            this.timeSinceStart = 0;
        }

        /// <summary>
        /// Update timer with deltaTime. Return the normalized Time.
        /// </summary>
        /// <param name="deltaTime">Duration of previous frame</param>
        /// <returns>Normalized Time after <paramref name="deltaTime"/></returns>
        public float Update(float deltaTime)
        {
            timeSinceStart += deltaTime;
            return NormalizedTime;
        }

        public bool IsWarnedTimeOver => indicatorDuration < 0 || timeSinceStart >= indicatorDuration;
        public float NormalizedTime => Mathf.Clamp01(timeSinceStart / indicatorDuration);
        public float TimeSinceStart => timeSinceStart;
    }
}