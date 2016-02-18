namespace Assets.Scripts {
    using UnityEngine;

    public class UnitySingleton<T> : MonoBehaviour
        where T : MonoBehaviour {

        public static bool WasDestroyed { get; private set; }

        private static T _instance;

        private static object _lockObject = new object();

        public static T Instance {
            get {
                if (_instance == null) {
                    lock (_lockObject) {
                        _instance = FindObjectOfType<T>();
                        if (_instance == null) {
                            var gameObject = new GameObject("Singleton: " + typeof(T));
                            _instance = gameObject.AddComponent<T>();
                        }
                    }
                }

                return _instance;
            }
        }

        private void Awake() {
            if (_instance && _instance.gameObject != gameObject) {
                DestroyImmediate(gameObject);
            }
            else {
                _instance = GetComponent<T>();
                DontDestroyOnLoad(gameObject);
                LateAwake();
            }
        }

        protected virtual void OnDestroy() {
            WasDestroyed = true;
        }

        protected virtual void LateAwake() {
        }

        public virtual void Init() {
        }
    }
}