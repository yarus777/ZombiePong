using Assets.Scripts.Effects;

namespace Assets.Scripts.Menu {
    using Game;
    using Popups;
    using UnityEngine;

    class GameUI : MonoBehaviour {
        public void Pause() {
            SoundManager.Instance.Play(FxType.MenuButton);
            GameLogic2d.Instance.Pause();
            PopupsController.Instance.Show(PopupType.Pause);
        }

        public void ShowSettings() {
            SoundManager.Instance.Play(FxType.MenuButton);
            GameLogic2d.Instance.Pause();
            PopupsController.Instance.Show(PopupType.Settings);
        }
    }
}