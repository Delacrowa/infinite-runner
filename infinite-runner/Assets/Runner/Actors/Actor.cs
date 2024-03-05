using UnityEngine;

namespace Runner.Actors
{
    /// <summary>
    ///     The core container of components that are intended to represent different behaviour. Contains API to retrieve them
    ///     and execute commands in fire-and-forget mode
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Actor : MonoBehaviour, IActor
    {
        public void Execute(IActorCommand command) =>
            command.Execute(this);

        public void Initialize()
        {
            foreach (var component in GetComponentsInChildren<IActorComponent>())
            {
                component.Initialize(this);
            }
        }

        public bool TryGet<T>(out T component) where T : IActorComponent
        {
            component = GetComponentInChildren<T>();
            return component != null;
        }
    }
}