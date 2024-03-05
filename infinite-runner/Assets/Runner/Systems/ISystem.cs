namespace Runner.Systems
{
    /// <summary>
    ///     Implement to create a system
    /// </summary>
    public interface ISystem
    {
        void Activate();

        void Deactivate();
    }
}