using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runner.Actors.Components.Jump
{
    /// <summary>
    ///     Manipulates actor movement and relative movement animations
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(Rigidbody2D))]
    public sealed class JumpComponent : MonoBehaviour, IActorComponent
    {
        private static readonly int Idle = Animator.StringToHash("isLookUp");
        private static readonly int Run = Animator.StringToHash("isRun");
        private static readonly int Jump = Animator.StringToHash("isJump");
        [SerializeField] private JumpComponentSettings _settings;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        private Transform _transform;
        private IJumpInput _input = NoInput.Instance;

        public void ChangeInput(IJumpInput input)
        {
            _input = input;
        }

        public void Initialize(IActor owner)
        {
            _transform = transform;
            JumpAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask JumpAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var grounded = Physics2D.Linecast(_transform.position, _groundCheck.position, _settings.Ground);
                if (_input.IsActive && grounded)
                {
                    _rigidbody.AddForce(Vector2.up * _settings.Force);
                }
                UpdateAnimation(grounded);
                await UniTask.NextFrame(token);
            }
        }

        private void UpdateAnimation(bool grounded)
        {
            if (_input.IsDisabled)
            {
                _animator.SetBool(Jump, false);
                _animator.SetBool(Run, false);
                _animator.SetBool(Idle, true);
                return;
            }
            switch (grounded)
            {
                case true when !_animator.GetBool(Run):
                    _animator.SetBool(Idle, false);
                    _animator.SetBool(Jump, false);
                    _animator.SetBool(Run, true);
                    break;
                case false when !_animator.GetBool(Jump):
                    _animator.SetBool(Idle, false);
                    _animator.SetBool(Jump, true);
                    _animator.SetBool(Run, false);
                    break;
            }
        }
    }
}