namespace Runner.Actors.Components.Jump
{
    /// <summary>
    ///     Disables all jump input. Use when actor should not react on any input
    /// </summary>
    public sealed class NoInput : IJumpInput
    {
        public bool IsDisabled => true;
        public bool IsActive => false;
        public static readonly IJumpInput Instance = new NoInput();
    }
}