namespace Assets.Scripts.Game {
    using UnityEngine;
    using UnityEngine.UI;

    class ScoreVisualizer : MonoBehaviour {
        [SerializeField]
        private Text _score;

        private void Awake() {
            GameLogic2d.Instance.ScoreChanged += UpdateScore;
        }

        private void UpdateScore(int score) {
            _score.text = "" + score;
        }
    }
}