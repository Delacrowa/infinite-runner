using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runner.Systems
{
    /// <summary>
    ///     Parallax movement implementation. Supports looping, speed changes and different movement directions.
    /// </summary>
    public sealed class ParallaxMovementSystem : MonoBehaviour, ISystem
    {
        [SerializeField] private Vector2 _speed;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private bool _isLooping;
        private Transform _transform;
        private Queue<(Transform Transform, Renderer Renderer)> _elements;
        private Camera _camera;
        private bool _active;

        public void Activate()
        {
            _active = true;
        }

        public void Deactivate()
        {
            _active = false;
        }

        private void Start()
        {
            _camera = Camera.main;
            _transform = transform;

            if (!_isLooping)
            {
                return;
            }

            var elements = new List<(Transform Transform, Renderer Renderer)>();

            for (var i = 0; i < _transform.childCount; i++)
            {
                var t = _transform.GetChild(i);
                var r = t.GetComponent<SpriteRenderer>();
                if (r)
                {
                    elements.Add((t, r));
                }
            }

            _elements = new Queue<(Transform Transform, Renderer Renderer)>(elements.OrderBy(t => t.Transform.position.x));
        }

        private void Update()
        {
            if (!_active)
            {
                return;
            }

            _transform.Translate(new Vector2(_speed.x * _direction.x, _speed.y * _direction.y) * Time.deltaTime);

            if (!_isLooping || _elements.Count < 2)
            {
                return;
            }

            var first = _elements.First();

            if (first.Transform.position.x > _camera.transform.position.x || first.Renderer.isVisible)
            {
                return;
            }

            var last = _elements.Last();

            var lastPosition = last.Transform.position;
            var lastBounds = last.Renderer.bounds;
            var lastSize = lastBounds.max - lastBounds.min;
            var firstPosition = first.Transform.position;

            first.Transform.position = new Vector3(lastPosition.x + lastSize.x, firstPosition.y, firstPosition.z);

            _elements.Enqueue(_elements.Dequeue());
        }
    }
}