using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runner.Systems.Spawn
{
    /// <summary>
    ///     Spawns elements depending on settings. Uses pooling to reduce instantiating overhead
    /// </summary>
    public sealed class SpawnSystem : MonoBehaviour, ISystem
    {
        [SerializeField] private SpawnSettings _settings;
        [SerializeField] private Transform[] _coordinates;
        [SerializeField] private Transform _location;
        [SerializeField] private SpawnPool _pool;
        [SerializeField, Range(1, 10)] private float _initialDelay;
        [SerializeField, Range(1, 10)] private float _minDelay;
        [SerializeField, Range(1, 10)] private float _maxDelay;
        private CancellationTokenSource _cancellation;

        public void Activate()
        {
            _cancellation?.Cancel();
            _cancellation = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
            RunAsync(_cancellation.Token).Forget();
        }

        public void Deactivate()
        {
            _cancellation?.Cancel();
            SpawnPool.Collect(_location);
        }

        private async UniTask RunAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_initialDelay), cancellationToken: token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(_minDelay, _maxDelay)), cancellationToken: token);

                if (token.IsCancellationRequested)
                {
                    return;
                }

                Spawn();
            }
        }

        private void Spawn()
        {
            var template = _settings.Next();
            var instance = _pool.Pop(template.Key, template.Entry);
            instance.transform.parent = _location;
            instance.transform.SetPositionAndRotation(_coordinates[Random.Range(0, _coordinates.Length)].position, Quaternion.identity);
            instance.SetActive(true);
        }
    }
}