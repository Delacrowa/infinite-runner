namespace Runner.Actors.Components.Effects.Commands
{
    /// <summary>
    ///     Cancels effect that is currently applied on actor if any
    /// </summary>
    public sealed class CancelEffectCommand : IActorCommand
    {
        public void Execute(IActor target)
        {
            if (target.TryGet(out EffectsComponent component))
            {
                component.Cancel();
            }
        }
    }
}