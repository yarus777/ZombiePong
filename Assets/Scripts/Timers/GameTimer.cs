using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Timers
{
    public class GameTimer : MonoBehaviour
    {
        public Text timerLabel;
        public float time;

        public bool isTimerActive;

        private GameLogic2d _game;

        void Update()
        {
            if (isTimerActive)
            {
                time += Time.deltaTime;

                var minutes = time/60;
                var seconds = time%60;
                timerLabel.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            }

            if (PlayerPrefs.GetInt("timer", 1) == 0)
                timerLabel.gameObject.SetActive(false);
            else
                timerLabel.gameObject.SetActive(true);
        }

        public void StopTimer()
        {
            isTimerActive = false;
        }

        void Awake()
        {
            
            _game = GameLogic2d.Instance;
            _game.StateChanged += OnStateChanged;
        }


        private void OnStateChanged(GameLogic2d.State state)
        {
            if (state == GameLogic2d.State.Game)
            {
                time = 0;
                isTimerActive = true;
                
            }
            else if (state == GameLogic2d.State.GameOver)
            {
                StopTimer();
            }
        }
    }
}
