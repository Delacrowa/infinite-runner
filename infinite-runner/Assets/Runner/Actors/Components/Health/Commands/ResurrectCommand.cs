using System;
using UnityEngine;

namespace Runner.Actors.Components.Health.Commands
{
    /// <summary>
    ///     Resurrects actor with the defined health amount if it matches the criteria
    /// </summary>
    [Serializable]
    public sealed class ResurrectCommand : IActorCommand
    {
        [SerializeField] private int _amount;

        public ResurrectCommand() : this(1)
        {
        }

        public ResurrectCommand(int delta) =>
            _amount = delta;

        public void Execute(IActor target)
        {
            if (target.TryGet(out HealthComponent component))
            {
                component.Resurrect(_amount);
            }
        }
    }
}