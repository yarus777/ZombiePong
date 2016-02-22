namespace Assets.Scripts.Currency {
    using System;
    using UnityEngine;

    class Currency : UnitySingleton<Currency> {
        private const string KEY = "coins_currency";
        private int coins_currency;

        public int CoinsCount {
            get { return coins_currency; }
            private set {
                coins_currency = value;
                OnCoinCountChanged();
                Save();
            }
        }

        protected override void LateAwake() {
            base.LateAwake();
            Load();
        }

        public void AddCoins() {
            CoinsCount += 2;
        }

        private void Load() {
            CoinsCount = PlayerPrefs.GetInt(KEY, 0);
        }

        private void Save() {
            PlayerPrefs.SetInt(KEY, CoinsCount);
            PlayerPrefs.Save();
        }

        public event Action<int> CoinCountChanged;

        private void OnCoinCountChanged() {
            if (CoinCountChanged != null) {
                CoinCountChanged(CoinsCount);
            }
        }
    }
}