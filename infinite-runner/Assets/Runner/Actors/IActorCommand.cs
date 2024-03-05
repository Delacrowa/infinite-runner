namespace Runner.Actors
{
    /// <summary>
    ///     Implement to create actor command
    /// </summary>
    public interface IActorCommand
    {
        void Execute(IActor target);
    }
}