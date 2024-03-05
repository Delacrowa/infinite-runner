using Runner.Actors;
using Runner.Actors.Components.Wallet;
using TMPro;
using UnityEngine;

namespace Runner.Menu
{
    /// <summary>
    ///     HUD that is shown while playing the game
    /// </summary>
    public sealed class HudMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balance;
        private WalletComponent _wallet;

        public void Hide()
        {
            _wallet.BalanceUpdate -= OnBalanceUpdate;
        }

        public void Show(IActor player)
        {
            if (_wallet)
            {
                Initialize(_wallet);
            }
            else if (player.TryGet(out _wallet))
            {
                Initialize(_wallet);
            }
            else
            {
                Debug.LogError($"No component {typeof(WalletComponent)} found on player");
            }
        }

        private void Initialize(WalletComponent component)
        {
            _wallet = component;
            _wallet.BalanceUpdate += OnBalanceUpdate;
            UpdateBalance(_wallet.Balance);
        }

        private void OnBalanceUpdate(int current)
        {
            UpdateBalance(current);
        }

        private void UpdateBalance(int balance)
        {
            _balance.text = balance.ToString();
        }
    }
}