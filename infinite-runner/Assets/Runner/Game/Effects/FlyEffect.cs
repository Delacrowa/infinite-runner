using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runner.Actors;
using Runner.Actors.Components.Effects;
using Runner.Actors.Components.Fly;
using UnityEngine;

namespace Runner.Game.Effects
{
    /// <summary>
    ///     Makes actor to fly for the defined period of time
    /// </summary>
    [Serializable]
    public sealed class FlyEffect : IEffect
    {
        [SerializeField] private float _duration;
        private CancellationTokenSource _cancellation;

        public FlyEffect(float duration) =>
            _duration = duration;

        public FlyEffect() : this(1)
        {
        }

        public void Cancel()
        {
            _cancellation?.Cancel();
        }

        public object Clone() =>
            new FlyEffect(_duration);

        public UniTask ExecuteAsync(IActor owner, CancellationToken token)
        {
            if (owner.TryGet(out FlyComponent component))
            {
                Cancel();
                _cancellation = CancellationTokenSource.CreateLinkedTokenSource(token);
                component.Fly(_duration, _cancellation.Token);
            }
            return UniTask.CompletedTask;
        }
    }
}