using System;
using Runner.Actors;
using Runner.Systems.Spawn;

namespace Runner.Game.Commands
{
    /// <summary>
    ///     Releases pooled object if it matches the criteria
    /// </summary>
    [Serializable]
    public sealed class HideObjectCommand : IActorCommand
    {
        public void Execute(IActor target)
        {
            if (target is not Actor actor)
            {
                return;
            }
            var element = actor.GetComponent<SpawnPoolElement>();
            if (element)
            {
                element.Release();
            }
        }
    }
}