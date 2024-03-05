using UnityEngine;

namespace Runner.Actors.Components.Jump
{
    /// <summary>
    ///     Platform dependent input listener
    /// </summary>
    public sealed class RegularInput : IJumpInput
    {
        public bool IsDisabled => false;
        public bool IsActive => Input.GetKeyDown(_code) || Input.touches.Length > 0;
        private readonly KeyCode _code;

        public RegularInput(KeyCode code) =>
            _code = code;
    }
}