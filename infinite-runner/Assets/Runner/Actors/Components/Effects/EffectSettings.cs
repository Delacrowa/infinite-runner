using System.Collections.Generic;
using UnityEngine;

namespace Runner.Actors.Components.Effects
{
    /// <summary>
    ///     Collection of effects that will be applied on target actor
    /// </summary>
    [CreateAssetMenu(menuName = MenuName, fileName = FileName)]
    public sealed class EffectSettings : ScriptableObject
    {
        private const string MenuName = "Settings/" + FileName;
        private const string FileName = nameof(EffectSettings);
        [SerializeReference, SubclassSelector] private List<IEffect> _effects = new();

        public void ApplyTo(IActor target)
        {
            if (target.TryGet(out EffectsComponent component))
            {
                for (int i = 0, max = _effects.Count; i < max; i++)
                {
                    component.Apply(_effects[i]);
                }
            }
        }
    }
}