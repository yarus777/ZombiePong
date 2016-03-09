using System;
using Assets.Scripts.Timers;
using JetBrains.Annotations;

namespace Assets.Scripts.Popups {
    using Effects;
    using Game;
    using UnityEngine;
    using UnityEngine.UI;

    class GameOverPopup : Popup {
        [SerializeField]
        private Text _scoreText;

        [SerializeField]
        private Text _highScoreText;

        [SerializeField]
        private Text timeText;

        [SerializeField]
        private LoadLevel _loadLevel;

        public GameTimer timer; 

        public override void OnShow() {
            base.OnShow();
            _scoreText.text = GameLogic2d.Instance.GetCurrentScore() + "";
            _highScoreText.text = GameLogic2d.Instance.GetBestScore() + "";

            ShowTime();

            AdSDK.AdSDK.CreateBanner();
            AdSDK.AdSDK.SetBannerVisible(true);

        }

        private void ShowTime()
        {
            var minutes = timer.time / 60;
            var seconds = timer.time % 60;
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        public void GoToMenu() {
            Close();
            _loadLevel.LoadingLevel("Menu");
            AdSDK.AdSDK.SetBannerVisible(false);
        }

        public override void OnBackClick() {
            Restart();
        }


        public void Restart() {
            Close();
            SoundManager.Instance.Play(FxType.MenuButton);
            GameLogic2d.Instance.RestartGame();
            AdSDK.AdSDK.SetBannerVisible(false);
        }

        public void Menu() {
            Close();
            SoundManager.Instance.Play(FxType.MenuButton);
            Application.LoadLevel("Menu");
            AdSDK.AdSDK.SetBannerVisible(false);
        }
    }
}