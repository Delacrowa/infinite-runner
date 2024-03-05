using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Menu
{
    /// <summary>
    ///     Menu that is shown when starting the new game or restarting because of the game over
    /// </summary>
    public sealed class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button _start;

        public void Hide()
        {
            _start.onClick.RemoveAllListeners();
        }

        public void Show(Action onStartClicked)
        {
            _start.onClick.AddListener(onStartClicked.Invoke);
        }
    }
}