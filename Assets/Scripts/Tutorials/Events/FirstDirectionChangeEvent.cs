namespace Assets.Scripts.Tutorials.Events {
    using Game;
    using UnityEngine;

    class FirstDirectionChangeEvent : TutorialEvent {
        [SerializeField]
        private PaddlePivot2d _paddle;

        private bool _fired;

        private void Awake() {
            _paddle.Rotated += OnRotated;
        }

        private void OnRotated() {
            if (!_fired) {
                OnFired();
                _fired = true;
            }
        }
    }
}