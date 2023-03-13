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
		private bool _isOver = false;
		private bool _isAttacked = false;
		public bool IsOver => _isActivated && _isOver;

        [SerializeField]
        private float indicatorDuration = 3f;
		[SerializeField]
		private float effectCd = 0.2f;
		[SerializeField]
        private float despawnAfterSecond = 0f;
		[SerializeField]
		private float attackDamage = 30f;
		[SerializeField]
		private ParticleSystem indicator;
		[SerializeField]
		private ParticleSystem effect;
		[SerializeField]
		private Collider2D _collider;
        private Collider2D[] _cachePlayer = new Collider2D[1];

        private float originalX;

		public void Activate()
        {
            _isActivated = true;
            if (Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x <= originalX)
            {
                // flip
                effect.GetComponent<ParticleSystemRenderer>().flip = Vector3.right;
            }
            effect.Play(true);
		}

        public void CleanUp()
        {
            _timer = null;
            indicator.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            effect.Stop(true);
		}

        public void OnStart()
        {
            _timer = new QteTimer(indicatorDuration);
            _isActivated = false;
            _isOver = false;
            _isAttacked = false;
			var position = Camera.main.ViewportToWorldPoint(Vector3.one / 2f);
            originalX = position.x;
			position.z = 0;
			transform.position = position;
			indicator.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			indicator.Play(true);
		}

        public void OnUpdate(float deltaTime)
        {
            if (_isOver)
                return;
            // Still in warning time
            if (_timer.Update(deltaTime) < 1f)
            {

            }
            else if (!_isActivated)
            {
                Activate();
            }
            else if (_collider.enabled && _timer.TimeSinceStart <= indicatorDuration + effectCd)
            {
				if (_collider.OverlapCollider(new ContactFilter2D { useLayerMask = true, useTriggers = true, layerMask = LayerMask.GetMask("Player"), }, _cachePlayer) > 0)
				{
                    _cachePlayer[0].GetComponent<Core.PlayerController>().TakeDamage(attackDamage);
                    _collider.enabled = false;
					_isAttacked = true;
				}
			}
            else if (_timer.TimeSinceStart > despawnAfterSecond + indicatorDuration)
            {
                CleanUp();
                _isOver = true;
			}
        }
    }
}
