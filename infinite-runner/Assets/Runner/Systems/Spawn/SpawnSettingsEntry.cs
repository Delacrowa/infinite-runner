using System;
using UnityEngine;

namespace Runner.Systems.Spawn
{
    /// <summary>
    ///     Settings for element that is spawned
    /// </summary>
    [Serializable]
    public sealed class SpawnSettingsEntry
    {
        public GameObject Entry => _entry;
        public int Weight => _weight;
        public string Key => _key;
        [SerializeField] private string _key;
        [SerializeField] private GameObject _entry;
        [SerializeField] private int _weight;
    }
}