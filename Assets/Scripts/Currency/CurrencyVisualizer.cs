namespace Assets.Scripts.Currency {
    using UnityEngine;
    using UnityEngine.UI;

    class CurrencyVisualizer : MonoBehaviour {
        [SerializeField]
        private Text _coinsCount;

        private Currency _currency;

        private void Awake() {
            _currency = Currency.Instance;
            _currency.CoinCountChanged += OnCoinCountChanged;
            OnCoinCountChanged(_currency.CoinsCount);
        }

        private void OnCoinCountChanged(int currentCount) {
            _coinsCount.text = currentCount + "";
        }

        private void OnDestroy() {
            _currency.CoinCountChanged -= OnCoinCountChanged;
        }
    }
}