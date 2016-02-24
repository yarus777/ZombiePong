namespace Assets.Scripts.Game {
    using UnityEngine;
    using UnityEngine.UI;

    class ScoreVisualizer : MonoBehaviour {
        [SerializeField]
        private Text _score;

        private GameLogic2d _game;

        private void Awake() {
            _game = GameLogic2d.Instance;
            _game.ScoreChanged += UpdateScore;
            _game.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(GameLogic2d.State state) {
            if (state == GameLogic2d.State.Game) {
                UpdateScore(0);
            }
        }

        private void UpdateScore(int score) {
            _score.text = "" + score;
        }
    }
}