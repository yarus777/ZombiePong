using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Currency
{
    class SpawnedCoin : MonoBehaviour
    {
        public event Action<SpawnedCoin> Collected;

        private void OnCollect()
        {
            if (Collected != null)
            {
                Collected(this);
            }
        }


        void OnTriggerEnter2D(Collider2D c)
        {
            if (c.CompareTag("BALL"))
            {
                Debug.Log("Ball Enter");
                OnCollect();
            }
           
        }
    }
}
