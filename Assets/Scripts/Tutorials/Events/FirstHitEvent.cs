namespace Assets.Scripts.Tutorials.Events {
    using Game;
    using UnityEngine;

    class FirstHitEvent : TutorialEvent {
        [SerializeField]
        private Ball2d _ball;

        private bool _fired;

        private void Awake() {
            _ball.Hit += OnHit;
        }

        private void OnHit() {
            if (!_fired) {
                OnFired();
                GameLogic2d.Instance.IsInputEnabled = true;
                _fired = true;
            }
        }
    }
}