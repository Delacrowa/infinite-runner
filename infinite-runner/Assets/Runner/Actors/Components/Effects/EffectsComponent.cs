using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runner.Actors.Components.Effects
{
    /// <summary>
    ///     Stores and applies effects on actor
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class EffectsComponent : MonoBehaviour, IActorComponent
    {
        private IActor _owner;
        private CancellationToken _token;
        private IEffect _current;

        public void Apply(IEffect effect)
        {
            Cancel();
            _current = (IEffect) effect.Clone();
            _current.ExecuteAsync(_owner, _token);
        }

        public void Cancel()
        {
            _current?.Cancel();
        }

        public void Initialize(IActor owner)
        {
            _owner = owner;
            _token = this.GetCancellationTokenOnDestroy();
        }
    }
}