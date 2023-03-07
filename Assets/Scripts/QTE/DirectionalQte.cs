using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QTE
{
    public abstract class DirectionalQte : MonoBehaviour, IQte
    {
        private QteTimer _timer;
        public float Progress => _timer == null ? 0 : _timer.NormalizedTime;

        private bool _isOver = false;
        public bool IsOver => _isOver;

        [SerializeField]
        private float indicatorDuration = 3f;

        public void Activate()
        {
            ;
        }

        public void CleanUp()
        {
            _timer = null;
        }

        public void OnStart()
        {
            _timer = new QteTimer(indicatorDuration);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_timer != null)
                _timer.Update(deltaTime);
        }
    }
}