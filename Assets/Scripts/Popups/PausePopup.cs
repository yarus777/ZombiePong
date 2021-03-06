﻿namespace Assets.Scripts.Popups {
    using Effects;
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

        public void Menu() {
            Close();
            SoundManager.Instance.Play(FxType.MenuButton);
            Application.LoadLevel("Menu");
        }

        public override void OnBackClick() {
            Resume();
        }
    }
}