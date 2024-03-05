using System.Collections.Generic;
using RSG;
using Runner.Actors;
using Runner.Actors.Components.Effects.Commands;
using Runner.Actors.Components.Health;
using Runner.Actors.Components.Health.Commands;
using Runner.Actors.Components.Jump;
using Runner.Actors.Components.Jump.Commands;
using Runner.Actors.Components.Wallet.Commands;
using Runner.Menu;
using Runner.Systems;
using Runner.Systems.Spawn;
using UnityEngine;

namespace Runner
{
    /// <summary>
    ///     Game entry point. Contains game loop state machine and manipulates the entire game state
    /// </summary>
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private Actor _player;
        [SerializeField] private SpawnSystem _spawnSystem;
        [SerializeField] private ParallaxMovementSystem[] _parallaxSystems;
        [SerializeField] private GameWindow _gameWindow;
        private readonly List<ISystem> _systems = new();
        private string Game = nameof(Game);
        private string Menu = nameof(Menu);
        private HealthComponent _health;
        private IState _machine = new StateMachineBuilder().Build(); //TODO: remove?

        private IState CreateGameLoop()
        {
            return new StateMachineBuilder()
                .State(Menu)
                .Enter(s =>
                {
                    SetSystemsActive(false);
                    _gameWindow.PlayClicked += OnPlayClicked;
                    _gameWindow.ShowStartGameWindow();
                })
                .Exit(s =>
                {
                    _gameWindow.PlayClicked -= OnPlayClicked;
                    _gameWindow.HideStartGameWindow();
                })
                .End()
                .State(Game)
                .Enter(s =>
                {
                    _player.Execute(new CancelEffectCommand());
                    _player.Execute(new ResurrectCommand(1));
                    _player.Execute(new ReduceBalanceCommand(int.MaxValue));
                    _player.Execute(new ChangeJumpInputCommand(new RegularInput(KeyCode.Space)));
                    _gameWindow.ShowGameHud(_player);
                    SetSystemsActive(true);
                })
                .Update((s, _) =>
                {
                    if (_health.IsDead)
                    {
                        s.Parent.ChangeState(Menu);
                    }
                })
                .Exit(s =>
                {
                    _player.Execute(new ChangeJumpInputCommand(NoInput.Instance));
                    _player.Execute(new CancelEffectCommand());
                    _gameWindow.HideGameHud();
                    SetSystemsActive(false);
                })
                .End()
                .Build();
        }

        private void OnPlayClicked()
        {
            _machine.ChangeState(Game);
        }

        private void SetSystemsActive(bool active)
        {
            for (int i = 0, max = _systems.Count; i < max; i++)
            {
                if (active)
                {
                    _systems[i].Activate();
                }
                else
                {
                    _systems[i].Deactivate();
                }
            }
        }

        private void Start()
        {
            _systems.Add(_spawnSystem);
            _systems.AddRange(_parallaxSystems);
            _player.Initialize();

            if (_player.TryGet(out _health))
            {
                _machine = CreateGameLoop();
                _machine.ChangeState(Menu);
            }
            else
            {
                Debug.LogError($"No {nameof(HealthComponent)} found on player");
            }
        }

        private void Update()
        {
            _machine.Update(Time.deltaTime);
        }
    }
}