namespace Runner.Actors.Components.Jump
{
    /// <summary>
    ///     Implement to create custom jump input listener
    /// </summary>
    public interface IJumpInput
    {
        bool IsDisabled { get; }
        bool IsActive { get; }
    }
}