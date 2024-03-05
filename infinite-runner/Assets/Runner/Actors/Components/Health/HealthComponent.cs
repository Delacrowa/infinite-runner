using System;
using UnityEngine;

namespace Runner.Actors.Components.Health
{
    /// <summary>
    ///     Manipulates actor health
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class HealthComponent : MonoBehaviour, IActorComponent
    {
        public bool IsDead => _health == 0;
        private int _health;

        public void Damage(int amount)
        {
            _health = Math.Clamp(_health - amount, 0, int.MaxValue);
        }

        public void Initialize(IActor owner)
        {
        }

        public void Resurrect(int amount)
        {
            _health = Math.Clamp(amount, 0, int.MaxValue);
        }
    }
}