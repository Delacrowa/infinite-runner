namespace Runner.Actors.Components.Jump.Commands
{
    /// <summary>
    ///     Switches actor input if it matches the criteria
    /// </summary>
    public sealed class ChangeJumpInputCommand : IActorCommand
    {
        private readonly IJumpInput _input;

        public ChangeJumpInputCommand(IJumpInput input) =>
            _input = input;

        public void Execute(IActor target)
        {
            if (target.TryGet(out JumpComponent component))
            {
                component.ChangeInput(_input);
            }
        }
    }
}