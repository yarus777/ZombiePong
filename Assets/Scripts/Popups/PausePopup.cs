using Assets.Scripts.Effects;

namespace Assets.Scripts.Popups {
    using Game;
    using UnityEngine;
    using UnityEngine.UI;

    class PausePopup : Popup {
        [SerializeField]
        private Text _score;

        public override void OnShow() {
            base.OnShow();
            _score.text = GameLogic2d.Instance.GetCurrentScore() + "";
        }

        public void Resume() {
            SoundManager.Instance.Play(FxType.MenuButton);
            Close();
            PopupsController.Instance.Show(PopupType.GetReady);
        }

        public void Menu()
        {
            SoundManager.Instance.Play(FxType.MenuButton);
            Application.LoadLevel("Menu");
        }

    }
}