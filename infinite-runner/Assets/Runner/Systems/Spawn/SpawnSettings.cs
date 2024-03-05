using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runner.Systems.Spawn
{
    /// <summary>
    ///     Settings for spawning entities. Current implementation is based on the random weighted selection when element with
    ///     the largest weight has more chances to be selected. Elements that has zero weight are ignored during the selection
    /// </summary>
    [CreateAssetMenu(menuName = MenuName, fileName = FileName)]
    public sealed class SpawnSettings : ScriptableObject
    {
        private const string MenuName = "Settings/" + FileName;
        private const string FileName = nameof(SpawnSettings);
        [SerializeField] private List<SpawnSettingsEntry> _entries = new();

        public (string Key, GameObject Entry) Next()
        {
            var total = _entries.Sum(e => e.Weight);
            var threshold = Random.Range(0, total + 1);

            foreach (var entry in _entries.Where(entry => entry.Weight != 0))
            {
                threshold -= entry.Weight;

                if (threshold <= 0)
                {
                    return (entry.Key, entry.Entry);
                }
            }

            throw new Exception($"Failed to select element in {name}");
        }
    }
}