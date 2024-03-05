using System;
using UnityEngine;

namespace Runner.Actors.Components.Wallet.Commands
{
    /// <summary>
    ///     Reduces wallet balance by the defined amount
    /// </summary>
    [Serializable]
    public sealed class ReduceBalanceCommand : IActorCommand
    {
        [SerializeField] private int _delta;

        public ReduceBalanceCommand() : this(0)
        {
        }

        public ReduceBalanceCommand(int delta) =>
            _delta = delta;

        public void Execute(IActor target)
        {
            if (target.TryGet(out WalletComponent component))
            {
                component.Reduce(_delta);
            }
        }
    }
}