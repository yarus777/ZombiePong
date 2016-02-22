namespace Assets.Scripts.Currency {
    using UnityEngine;
    using UnityEngine.UI;

    class CurrencyVisualizer : MonoBehaviour {
        [SerializeField]
        private Text _coinsCount;

        private void Awake() {
            Currency.Instance.CoinCountChanged += OnCoinCountChanged;
        }

        private void OnCoinCountChanged(int currentCount) {
            _coinsCount.text = currentCount + "";
        }

        private void OnDestroy() {
            Currency.Instance.CoinCountChanged -= OnCoinCountChanged;
        }
    }
}