using UnityEngine;

namespace Runner.Actors.Components.Effects
{
    /// <summary>
    ///     Applies effect on collided entity if it is an actor
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class EffectTrigger : MonoBehaviour
    {
        [SerializeField] private EffectSettings _effect;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out IActor actor))
            {
                _effect.ApplyTo(actor);
            }
        }
    }
}