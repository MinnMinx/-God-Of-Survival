using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace QTE
{
    public class DirectionalQte : MonoBehaviour, IQte
    {
        private QteTimer _timer;
        public float Progress => _timer.NormalizedTime;

        private bool _isOver = false;
        public bool IsOver => _isOver;

		[SerializeField]
        private ParticleSystem spikes;
		[SerializeField]
		private Collider2D collider;
        private Collider2D[] _cachePlayer = new Collider2D[1];

		[SerializeField]
        private float despawnAfterSecond = 10f;

        [SerializeField]
        private float indicatorDuration = 3f;

        [SerializeField]
        private float speed = 10f;
		[SerializeField]
		private float attackDamage = 30f;

		[SerializeField]
        private LineRenderer indicator;
        private Vector3 directionalVector = Vector3.zero;

        private bool _isActivated = false;

        public void Activate() {
            indicator.enabled = false;
			_isActivated = true;
            // activate renderer
            spikes.Play();
		}

        public void CleanUp()
        {
            _timer = null;
			spikes.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}

        public void OnStart()
        {
            _timer = new QteTimer(indicatorDuration);
            _isOver = false;
            _isActivated = false;
			spikes.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

			// Randomize spawn position in view
			int horizontal = Random.value >= 0.5f ? 0 : 1;
            var viewPortPos = new Vector2()
            {
                x = horizontal * Random.value,
                y = (1 - horizontal) * Random.value
            };
            var spawnPos = Camera.main.ViewportToWorldPoint(viewPortPos);
            var endPos = Camera.main.ViewportToWorldPoint(Vector2.one / 2f);
            directionalVector = (endPos - spawnPos).normalized;
            spawnPos.z = 0;
            endPos.z = 0;

            // Set the indicator
            if (indicator != null)
            {
                indicator.SetPositions(new Vector3[]
                {
                    spawnPos - directionalVector * 16, endPos + directionalVector * 24,
                });
            }

            transform.position = spawnPos - directionalVector * 8;
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
            else if (_timer.TimeSinceStart < despawnAfterSecond + indicatorDuration)
            {
                // Move object toward target position
                transform.position += directionalVector * speed * deltaTime;

                //detech collision
                if (collider.enabled && collider.OverlapCollider(new ContactFilter2D { useLayerMask = true, layerMask = 1<<6, useTriggers = true, }, _cachePlayer) > 0)
                {
                    _cachePlayer[0].GetComponent<Core.PlayerController>().TakeDamage(attackDamage);
                    collider.enabled = false;
				}
			}
            else
            {
				// Despawn
				spikes.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
				_isOver = true;
            }
        }
	}
}