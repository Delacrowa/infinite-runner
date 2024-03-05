using System;
using UnityEngine;

namespace Runner.Actors.Components.Wallet.Commands
{
    /// <summary>
    ///     Increases wallet balance by the defined amount
    /// </summary>
    [Serializable]
    public sealed class IncreaseBalanceCommand : IActorCommand
    {
        [SerializeField] private int _delta;

        public IncreaseBalanceCommand() : this(0)
        {
        }

        public IncreaseBalanceCommand(int delta) =>
            _delta = delta;

        public void Execute(IActor target)
        {
            if (target.TryGet(out WalletComponent component))
            {
                component.Increase(_delta);
            }
        }
    }
}