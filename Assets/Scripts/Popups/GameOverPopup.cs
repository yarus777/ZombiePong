using Assets.Scripts.Effects;

namespace Assets.Scripts.Popups {
    using Game;
    using UnityEngine;
    using UnityEngine.UI;

    class GameOverPopup : Popup {
        [SerializeField]
        private Text _scoreText;

        [SerializeField]
        private Text _highScoreText;

        public override void OnShow() {
            base.OnShow();
            _scoreText.text = GameLogic2d.Instance.GetCurrentScore() + "";
            _highScoreText.text = GameLogic2d.Instance.GetBestScore() + "";
        }


        public void Restart() {
            SoundManager.Instance.Play(FxType.MenuButton);
            GameLogic2d.Instance.RestartGame();
        }
    }
}