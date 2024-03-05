using System;
using UnityEngine;

namespace Runner.Actors.Components.Wallet
{
    /// <summary>
    ///     Stores and manipulates actor wallet balance
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class WalletComponent : MonoBehaviour, IActorComponent
    {
        public int Balance
        {
            get => _balance;
            private set => _balance = Math.Clamp(value, 0, int.MaxValue);
        }

        public event Action<int> BalanceUpdate = delegate { };
        private int _balance;

        public void Increase(int amount)
        {
            Balance += amount;
            BalanceUpdate.Invoke(Balance);
        }

        public void Initialize(IActor owner)
        {
        }

        public void Reduce(int amount)
        {
            Balance -= amount;
            BalanceUpdate.Invoke(Balance);
        }
    }
}