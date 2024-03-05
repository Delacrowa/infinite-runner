using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runner.Actors;
using Runner.Actors.Components.Effects;
using UnityEngine;

namespace Runner.Game.Effects
{
    /// <summary>
    ///     Changes actor speed by the defined amount for the defined time period. Current implementation uses time scale which
    ///     could be not suitable for all cases
    /// </summary>
    [Serializable]
    public sealed class ChangeSpeedEffect : IEffect
    {
        [SerializeField] private float _multiplier = 1;
        [SerializeField] private float _duration = 1;
        private CancellationTokenSource _source;

        public ChangeSpeedEffect(float multiplier, float duration)
        {
            _multiplier = multiplier;
            _duration = duration;
        }

        public ChangeSpeedEffect() : this(1, 1)
        {
        }

        public void Cancel()
        {
            _source?.Cancel();
            Time.timeScale = 1;
        }

        public object Clone() =>
            new ChangeSpeedEffect(_multiplier, _duration);

        public async UniTask ExecuteAsync(IActor owner, CancellationToken token)
        {
            Cancel();
            _source = CancellationTokenSource.CreateLinkedTokenSource(token);
            Time.timeScale = _multiplier;
            await UniTask.Delay(TimeSpan.FromSeconds(_duration), cancellationToken: _source.Token);
            Time.timeScale = 1;
        }
    }
}