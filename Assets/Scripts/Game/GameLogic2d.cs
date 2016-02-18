namespace Assets.Scripts.Game {
    using System;
    using Popups;
    using Tutorials;
    using UnityEngine;

    public class GameLogic2d : MonoBehaviour {
        public static GameLogic2d Instance;

        public enum State {
            Game,
            GameOver
        }

        private State _state;

        public float initialGameSpeed = 0.8f;
        public float gameIncrementSpeed = 0.01f;
        public float GameSpeed { get; private set; }

        public bool IsInputEnabled { get; set; }

        [Tooltip("This Is The Score When Game Is Playing")]
        private int _currentScore;

        [SerializeField]
        private PaddlePivot2d _paddlePivot;

        [SerializeField]
        private Ball2d _ball;

        [SerializeField]
        private Tutorial _tutorial;

        public event Action<int> ScoreChanged;

        void Awake() {
            Instance = this;
            Application.targetFrameRate = 60;
            _ball.Lost += GameOver;
            _tutorial.StepBecameActive += Pause;
            _tutorial.StepBecameInactive += ResumeGame;
        }

        void Start() {
            StartGame();
        }


        public void OnClick() {
            HandlePlayerInput();
        }

        #region PLAYER INPUT

        void HandlePlayerInput() {
            if (IsInputEnabled) {
                _paddlePivot.ChangeDirection();
            }
        }

        #endregion

        #region GAME LOGIC

        private void StartGame() {
            _currentScore = 0;
            GameSpeed = initialGameSpeed;
            OnStateChanged(State.Game);
            if (!_tutorial.IsFirstRun) {
                IsInputEnabled = true;
                Pause();
                PopupsController.Instance.Show(PopupType.GetReady);
            } else {
                ResumeGame();
            }
        }


        public void Pause() {
            Time.timeScale = 0;
        }

        public void ResumeGame() {
            Time.timeScale = 1;
        }

        public void RestartGame() {
            StartGame();
        }

        #endregion

        public void GameOver() {
            var bestScore = GetBestScore();
            if (_currentScore > bestScore) {
                SaveBestScore(_currentScore);
            }

            OnStateChanged(State.GameOver);
            PopupsController.Instance.Show(PopupType.GameOver);

            FullScreenHandler.ShowAds();
        }

        #region State

        public delegate void StateChangedDelegate(State state);

        public event StateChangedDelegate StateChanged;

        private void OnStateChanged(State state) {
            _state = state;
            var handler = StateChanged;
            if (handler != null) {
                handler(_state);
            }
        }

        #endregion

        #region SCORE

        public int GetCurrentScore() {
            return _currentScore;
        }

        public void AddScore(int score) {
            if (_currentScore < 5) {
                GameSpeed += gameIncrementSpeed*2f;
            } else if (_currentScore < 10) {
                GameSpeed += gameIncrementSpeed*1.5f;
            } else if (_currentScore < 20) {
                GameSpeed += gameIncrementSpeed*1f;
            } else if (_currentScore < 30) {
                GameSpeed += gameIncrementSpeed*0.8f;
            } else if (_currentScore < 40) {
                GameSpeed += gameIncrementSpeed*0.5f;
            }


            _currentScore += score;
            OnScoreChanged(_currentScore);
            Debug.Log("Score" + _currentScore + "\n Game Speed: " + GameSpeed);
        }

        private void OnScoreChanged(int score) {
            var handler = ScoreChanged;
            if (handler != null) {
                handler.Invoke(score);
            }
        }

        public int GetBestScore() {
            return PlayerPrefs.GetInt("BESTSCORE", 0);
        }

        public void SaveBestScore(int score) {
            PlayerPrefs.SetInt("BESTSCORE", score);
        }

        #endregion
    }
}