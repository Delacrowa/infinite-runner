using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runner.Actors.Components.Effects
{
    /// <summary>
    ///     Implement to create effect that can be applied on actor
    /// </summary>
    public interface IEffect : ICloneable
    {
        void Cancel();

        UniTask ExecuteAsync(IActor owner, CancellationToken token);
    }
}