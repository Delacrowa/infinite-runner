using UnityEngine;

namespace Runner.Actors.Components.Fly
{
    /// <summary>
    ///     Settings for fly component
    /// </summary>
    [CreateAssetMenu(menuName = MenuName, fileName = FileName)]
    public sealed class FlyComponentSettings : ScriptableObject
    {
        public float Height => _height;
        private const string MenuName = "Settings/" + FileName;
        private const string FileName = nameof(FlyComponentSettings);
        [SerializeField] private float _height;
    }
}