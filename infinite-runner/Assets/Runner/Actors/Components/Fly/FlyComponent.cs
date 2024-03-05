using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runner.Actors.Components.Fly
{
    /// <summary>
    ///     Manipulates actor flying behaviour
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(Rigidbody2D))]
    public sealed class FlyComponent : MonoBehaviour, IActorComponent
    {
        [SerializeField] private FlyComponentSettings _settings;
        [SerializeField] private Rigidbody2D _rigidbody;
        private Transform _transform;
        private Vector3 _position;
        private CancellationTokenSource _cancellation;

        public void Fly(float duration, CancellationToken token)
        {
            _cancellation?.Cancel();
            _cancellation = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy(), token);
            FlyAsync(TimeSpan.FromSeconds(duration), _cancellation.Token).Forget();
        }

        public void Initialize(IActor owner)
        {
            _transform = transform;
            _position = _transform.position;
        }

        private async UniTask FlyAsync(TimeSpan duration, CancellationToken token)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
            _transform.position = new Vector3(_position.x, _settings.Height, _position.z);
            await UniTask.Delay(duration, cancellationToken: token);
            _transform.position = _position;
            _rigidbody.isKinematic = false;
        }
    }
}