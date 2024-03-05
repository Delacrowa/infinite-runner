namespace Runner.Actors
{
    /// <summary>
    ///     Implement to create an actor
    /// </summary>
    public interface IActor
    {
        void Execute(IActorCommand command);

        void Initialize();

        bool TryGet<T>(out T component) where T : IActorComponent;
    }
}