namespace Assets.Scripts.Game {
    using System;
    using UnityEngine;

    class PaddlePivot2d : MonoBehaviour {
        public float rotateSpeed = 5f; // ROTATION SPEED

        [SerializeField]
        private float _initialRotation;

        private float _rotateDirection; // ROTATION DIRECTION

        private Transform _transform; // CACHED TRANSFORM

        void Awake() {
            _transform = GetComponent<Transform>();
            GameLogic2d.Instance.StateChanged += OnGameStateChanged;
            //Init();
        }

        private void OnGameStateChanged(GameLogic2d.State state) {
            switch (state) {
                case GameLogic2d.State.GameOver:
                    _rotateDirection = 0f;
                    break;
                case GameLogic2d.State.Game:
                    Init();
                    break;
                case GameLogic2d.State.Init:
                    InitPosition();
                    break;
            }
        }

        private void InitPosition() {
            if (GameLogic2d.Instance.IsFirstRun)
            {
                _transform.rotation = Quaternion.Euler(0, 0, _initialRotation);
            }
            else
            {                
                _transform.rotation = Quaternion.Euler(0, 0, 110);
            } 
           
        }

        private void Init() {
            _rotateDirection = 1f;
        }

        private void FixedUpdate() {
            if (_rotateDirection > 0f) {
                _transform.Rotate(Vector3.forward, -rotateSpeed*GameLogic2d.Instance.GameSpeed*Time.deltaTime);
            } else if (_rotateDirection < 0f) {
                _transform.Rotate(Vector3.forward, rotateSpeed*GameLogic2d.Instance.GameSpeed*Time.deltaTime);
            }
        }

        public void ChangeDirection() {
            _rotateDirection *= -1f;
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