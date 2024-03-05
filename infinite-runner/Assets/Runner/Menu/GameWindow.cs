using System;
using Runner.Actors;
using UnityEngine;

namespace Runner.Menu
{
    /// <summary>
    ///     The main entry point for all game UI. Contains high level logic
    /// </summary>
    public sealed class GameWindow : MonoBehaviour
    {
        [SerializeField] private StartMenu _start;
        [SerializeField] private HudMenu _hud;
        public event Action PlayClicked = delegate { };

        public void HideGameHud()
        {
            _hud.Hide();
            _hud.gameObject.SetActive(false);
        }

        public void HideStartGameWindow()
        {
            _start.Hide();
            _start.gameObject.SetActive(false);
        }

        public void ShowGameHud(IActor player)
        {
            _hud.Show(player);
            _hud.gameObject.SetActive(true);
        }

        public void ShowStartGameWindow()
        {
            _start.Show(PlayClicked);
            _start.gameObject.SetActive(true);
        }
    }
}