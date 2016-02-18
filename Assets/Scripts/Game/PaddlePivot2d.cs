namespace Assets.Scripts.Game {
    using System;
    using UnityEngine;

    class PaddlePivot2d : MonoBehaviour {
        public float rotateDirection; // ROTATION DIRECTION
        public float rotateSpeed = 5f; // ROTATION SPEED

        [SerializeField]
        private float _initialRotation;

        private Transform _transform; // CACHED TRANSFORM

        void Awake() {
            _transform = GetComponent<Transform>();
            GameLogic2d.Instance.StateChanged += state => {
                switch (state) {
                    case GameLogic2d.State.GameOver:
                        rotateDirection = 0f;
                        break;
                    case GameLogic2d.State.Game:
                        Init();
                        break;
                }
            };
            Init();
        }

        private void Init() {
            _transform.rotation = Quaternion.Euler(0, 0, _initialRotation);
            rotateDirection = 1f;
        }

        private void FixedUpdate() {
            if (rotateDirection > 0f) {
                _transform.Rotate(Vector3.forward, -rotateSpeed*GameLogic2d.Instance.GameSpeed*Time.deltaTime);
            } else if (rotateDirection < 0f) {
                _transform.Rotate(Vector3.forward, rotateSpeed*GameLogic2d.Instance.GameSpeed*Time.deltaTime);
            }
        }

        public void ChangeDirection() {
            rotateDirection *= -1f;
            OnRotated();
        }

        public event Action Rotated;

        private void OnRotated() {
            var handler = Rotated;
            if (handler != null) {
                handler();
            }
        }
    }
}