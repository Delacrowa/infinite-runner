using UnityEngine;

namespace Runner.Actors.Components.Jump
{
    /// <summary>
    ///     Settings for jump component
    /// </summary>
    [CreateAssetMenu(menuName = MenuName, fileName = FileName)]
    public sealed class JumpComponentSettings : ScriptableObject
    {
        public LayerMask Ground => _ground;
        public float Force => _force;
        private const string MenuName = "Settings/" + FileName;
        private const string FileName = nameof(JumpComponentSettings);
        [SerializeField] private LayerMask _ground;
        [SerializeField] private float _force;
    }
}