using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;



    class Currency : MonoBehaviour
    {
        public static Currency Instance;
        public Text coins;
        private int coins_currency;

        void Awake()
        {
            Instance = this;
            coins_currency = PlayerPrefs.GetInt("coins_currency", 0);
            coins.text = "" + coins_currency;
        }

        public void AddCoins()
        {
          
                coins_currency = coins_currency + 2;
                coins.text = "" + coins_currency;

                PlayerPrefs.SetInt("coins_currency", coins_currency);
                PlayerPrefs.Save();
            }




    }

