using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class TimerToggle : MonoBehaviour
    {

        public Toggle timerToggle;

        void Start()
        {
            timerToggle.isOn = PlayerPrefs.GetInt("timer", 1) == 1;
        }

        public void TimerToggleChanged(bool isChecked)
        {
            PlayerPrefs.SetInt("timer", isChecked ? 1: 0);
            PlayerPrefs.Save();

            Debug.Log("IsTimerChecked" + isChecked);

        }
    }
}
