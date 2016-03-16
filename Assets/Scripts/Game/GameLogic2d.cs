
using Assets.Scripts.Timers;
using UnityEngine.UI;

namespace Assets.Scripts.Game {
    using System;
    using Popups;
    using Tutorials;
    using UnityEngine;

    public class GameLogic2d : MonoBehaviour {
        public static GameLogic2d Instance;

        public enum State {
            Game,
            GameOver,
            Init
        }

        public State GameState { get; private set; }

        public float initialGameSpeed = 0.8f;
        public float gameIncrementSpeed = 0.01f;
        public float GameSpeed { get; private set; }

        public bool IsInputEnabled { get; set; }
        public bool IsTutorialActive { get; private set; }

        public bool IsFirstRun {
            get { return _tutorial.IsFirstRun; }
        }

        [Tooltip("This Is The Score When Game Is Playing")]
        private int _currentScore;

        [SerializeField]
        private PaddlePivot2d _paddlePivot;

        [SerializeField]
        private Ball2d _ball;

        [SerializeField]
        private Tutorial _tutorial;

        public event Action<int> ScoreChanged;

        private bool _fired;

        public GameTimer timer;

        void Awake() {
            Instance = this;
            Application.targetFrameRate = 60;
            PlayerPrefs.SetString(UpdatePopup.PREV_SCENE_NAME_KEY, "game");
            _ball.Lost += GameOver;
            _tutorial.StepBecameActive += OnTutorialBecameActive;
            _tutorial.StepBecameInactive += OnTutorialBecameInactive;

            _ball.Hit += OnFirstHit;
        }

        private void OnFirstHit()
        {
            if (!_fired)
            {
                GameLogic2d.Instance.IsInputEnabled = true;
                _fired = true;
            }
        }

        private void OnTutorialBecameActive() {
            Pause();
            IsTutorialActive = true;
        }

        private void OnTutorialBecameInactive() {
            ResumeGame();
            IsTutorialActive = false;
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

        private void StartGame()
        {
            _currentScore = 0;
            GameSpeed = initialGameSpeed;
            OnStateChanged(State.Init);
            if (!_tutorial.IsFirstRun) {
                IsInputEnabled = false;
                Pause();
                var popup = PopupsController.Instance.Show(PopupType.GetReady) as GetReadyPopup;
                popup.IsGameJustStarted = true;
            } else {
                ResumeGame();
            }
        }

        public void StartPlaying() {
            OnStateChanged(State.Game);
            ResumeGame();
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

        public void GameOver()
        {
            _fired = false;
            var bestScore = GetBestScore();
            if (_currentScore > bestScore) {
                SaveBestScore(_currentScore);
            }

            OnStateChanged(State.GameOver);
            PopupsController.Instance.Show(PopupType.GameOver);

            FullScreenHandler.ShowAds();

            SendScoreEvent();
            SendRoundEvent();
   
            PlayerPrefs.SetInt(Tutorial.FIRST_START_KEY, 1);
        }

        private void SendScoreEvent()
        {
            var min = _currentScore - _currentScore%10;
            var min1 = Math.Max(1, min);
            var max = min + 10;
            string score_to_send = string.Format("scores_{0:00000}-{1:00000}", min1, max);          
            Debug.Log("ScoreToSend "+ score_to_send);
            AdSDK.AdSDK.SendEvent(score_to_send);
        }

        private void SendRoundEvent()
        {
            LoseCounter.Instance.IncrementCount();
            string round_to_send = String.Format("round_{0:00000000}", LoseCounter.Instance.lose_count);
            Debug.Log("LoseRound " + round_to_send);
            AdSDK.AdSDK.SendEvent(round_to_send);
        }

        #region State

        public delegate void StateChangedDelegate(State state);

        public event StateChangedDelegate StateChanged;

        private void OnStateChanged(State state) {
            GameState = state;
            var handler = StateChanged;
            if (handler != null) {
                handler(GameState);
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
            //Debug.Log("Score" + _currentScore + "\n Game Speed: " + GameSpeed);
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