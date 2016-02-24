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
        private LoadLevel _loadLevel;

        public override void OnShow() {
            base.OnShow();
            _scoreText.text = GameLogic2d.Instance.GetCurrentScore() + "";
            _highScoreText.text = GameLogic2d.Instance.GetBestScore() + "";
        }

        public void GoToMenu() {
            Close();
            _loadLevel.LoadingLevel("Menu");
        }


        public void Restart() {
            Close();
            SoundManager.Instance.Play(FxType.MenuButton);
            GameLogic2d.Instance.RestartGame();
        }

        public void Menu() {
            Close();
            SoundManager.Instance.Play(FxType.MenuButton);
            Application.LoadLevel("Menu");
        }
    }
}