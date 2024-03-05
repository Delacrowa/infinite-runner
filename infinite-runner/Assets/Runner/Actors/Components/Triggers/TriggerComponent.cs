using System.Collections.Generic;
using UnityEngine;

namespace Runner.Actors.Components.Triggers
{
    /// <summary>
    ///     Applies a set of commands on the collided actor if it matches the criteria
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TriggerComponent : MonoBehaviour, IActorComponent
    {
        [SerializeReference, SubclassSelector] private List<IActorCommand> _commands = new();

        public void Initialize(IActor owner)
        {
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent(out IActor target))
            {
                foreach (var command in _commands)
                {
                    target.Execute(command);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IActor target))
            {
                foreach (var command in _commands)
                {
                    target.Execute(command);
                }
            }
        }
    }
}