namespace Assets.Scripts.Game {
    using UnityEngine;

    public class GameCamera : MonoBehaviour {
        public bool isShaking;

        public float shakeTimer;
        public float shakeDuration;
        public float shakeFrequency;
        public float shakeBounds;

        public Vector2 shakeStartDir;
        public Vector3 shakeOffset = Vector3.zero;

        public Vector3 initialPosition;

        public Transform tr;

        void Awake() {
            tr = GetComponent<Transform>();
            initialPosition = tr.position;
        }

        void LateUpdate() {
            // SHAKING
            if (isShaking) {
                shakeTimer += Time.deltaTime;
                if (shakeTimer > shakeDuration) {
                    isShaking = false;
                    shakeOffset = Vector3.zero;
                } else {
                    var __t = shakeTimer/shakeDuration;
                    var __freq = Mathf.Lerp(shakeFrequency, 4f*shakeFrequency, __t);
                    var __v = new Vector2(Mathf.Lerp(shakeBounds, 0f, __t)*Mathf.Sin(shakeTimer*__freq),
                        Mathf.Lerp(shakeBounds, 0f, __t)*Mathf.Sin((shakeTimer*__freq)*2f));
                    shakeOffset = new Vector3(__v.x*shakeStartDir.x, __v.y*shakeStartDir.y, 0f);
                }

                tr.position = new Vector3(initialPosition.x + shakeOffset.x, initialPosition.y + shakeOffset.y,
                    initialPosition.z);
            }
        }

        public void Shake(float _frequency, float _bounds, float _duration) {
            isShaking = true;
            shakeTimer = 0f;
            shakeDuration = _duration;
            shakeBounds = _bounds;
            shakeFrequency = _frequency;
            shakeStartDir = new Vector2(0.2f, 1f);
        }
    }
}