using System;
using UnityEngine;

namespace Runner.Actors.Components.Health.Commands
{
    /// <summary>
    ///     Applies damage on actor if it matches the criteria
    /// </summary>
    [Serializable]
    public sealed class DamageCommand : IActorCommand
    {
        [SerializeField] private int _amount;

        public DamageCommand() : this(0)
        {
        }

        public DamageCommand(int delta) =>
            _amount = delta;

        public void Execute(IActor target)
        {
            if (target.TryGet(out HealthComponent component))
            {
                component.Damage(_amount);
            }
        }
    }
}