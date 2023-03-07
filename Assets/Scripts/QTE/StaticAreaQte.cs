using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QTE
{
    public class StaticAreaQte : MonoBehaviour, IQte
    {
        private QteTimer _timer;
        public float Progress => _timer.NormalizedTime;

        private bool _isActivated = false;
        public bool IsOver => _isActivated && Progress == 1f;

        [SerializeField]
        private float indicatorDuration = 3f;
        [SerializeField]
        private float despawnAfterSecond = 0f;
        private Vector3 targetPos = Vector3.zero;

        public void Activate()
        {
            _isActivated = true;
            Destroy(gameObject, despawnAfterSecond);
        }

        public void CleanUp()
        {
            _timer = null;
        }

        public void OnStart()
        {
            _timer = new QteTimer(indicatorDuration);
            _isActivated = false;
        }

        public void OnUpdate(float deltaTime)
        {
            // Still in warning time
            if (_timer.Update(deltaTime) < 1f)
            {

            }
            else if (!_isActivated)
            {
                Activate();
            }
        }
    }
}
