using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Runner.Systems.Spawn
{
    /// <summary>
    ///     Auto release pool element. Contains API to manipulate pooled object automatically or manually
    /// </summary>
    public sealed class SpawnPoolElement : MonoBehaviour
    {
        private SpawnPool _pool;
        private string _key;
        private IDisposable _disposable;
        private CancellationToken _token;

        public void Initialize(string key, SpawnPool pool, float autoReleaseInterval)
        {
            _key = key;
            _pool = pool;
            _token = this.GetCancellationTokenOnDestroy();
            _disposable?.Dispose();
            _disposable = UniTaskAsyncEnumerable
                .Timer(TimeSpan.FromSeconds(autoReleaseInterval))
                .Subscribe(_ =>
                {
                    if (!_token.IsCancellationRequested)
                    {
                        Release();
                    }
                });
        }

        public void Release()
        {
            _disposable.Dispose();
            _pool.Release(_key, this);
        }
    }
}