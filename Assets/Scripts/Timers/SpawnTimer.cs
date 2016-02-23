using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Timers
{
    class SpawnTimer : MonoBehaviour
    {
        public event Action OnSpawnTimerTick;
        
        private void CoinAppear()
        {
            if (OnSpawnTimerTick != null)
            {
                OnSpawnTimerTick();
            }
        }


        public void Awake()
        {
            StartSpawnTimer();
            Debug.Log("StartTimer");
        }
        private float timeLeft;

        public void StartSpawnTimer()
        {
            timeLeft = Random.Range(10, 40);
        }

        public void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                CoinAppear();
                StartSpawnTimer();

            }
        }
    }
}
