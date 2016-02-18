namespace Assets.Scripts.Tutorials.Events {
    using Game;
    using UnityEngine;

    class FirstAngleReachedEvent : TutorialEvent {
        private const float TOLERANCE = 2f;

        [SerializeField]
        private float _angle;

        [SerializeField]
        private PaddlePivot2d _paddle;

        private bool _fired;

        private Transform _tr;

        private void Start() {
            _tr = _paddle.transform;
        }

        private void LateUpdate() {
            if (_fired) {
                return;
            }
            var abs = Mathf.Abs(_tr.localRotation.eulerAngles.z - _angle);
            Debug.Log(abs);
            if (abs < TOLERANCE) {
                OnFired();
                _fired = true;
            }
        }
    }
}