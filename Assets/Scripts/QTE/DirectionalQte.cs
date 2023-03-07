using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

namespace QTE
{
    public class DirectionalQte : MonoBehaviour, IQte
    {
        private QteTimer _timer;
        public float Progress => _timer.NormalizedTime;

        private bool _isOver = false;
        public bool IsOver => _isOver;

        [SerializeField]
        private SpriteRenderer[] objects;

        [SerializeField]
        private float despawnAfterSecond = 10f;

        [SerializeField]
        private float indicatorDuration = 3f;

        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private LineRenderer indicator;
        private Vector3 directionalVector = Vector3.zero;

        private bool _isActivated = false;
        public void Activate() {
            indicator.enabled = false;
            _isActivated = true;
            // activate renderer
            foreach (var spriteRenderer in objects)
            {
                spriteRenderer.enabled = true;
            }
        }

        public void CleanUp()
        {
            _timer = null;
        }

        public void OnStart()
        {
            _timer = new QteTimer(indicatorDuration);
            _isOver = false;
            _isActivated = false;

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
                    spawnPos - directionalVector * 8, endPos + directionalVector * 16,
                });
            }

            // Disable spawn objects
            foreach (var spriteRenderer in objects)
            {
                spriteRenderer.enabled = false;
                spriteRenderer.transform.position = spawnPos - directionalVector * 8 + spriteRenderer.transform.localPosition;
            }
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
                foreach(var sprRenderer in objects)
                {
                    sprRenderer.transform.position += directionalVector * speed * deltaTime;
                }
            }
            else
            {
                // Despawn
                foreach (var spriteRenderer in objects)
                {
                    spriteRenderer.enabled = false;
                }
                _isOver = true;
            }
        }
    }
}