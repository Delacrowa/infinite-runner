namespace Runner.Actors
{
    /// <summary>
    ///     Implement to create actor component
    /// </summary>
    public interface IActorComponent
    {
        void Initialize(IActor owner);
    }
}