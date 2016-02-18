using UnityEngine;

namespace Assets.Scripts {
    public class MyDebug : MonoBehaviour {
        public static void Log (object o) {
            if (log) Debug.Log ("MY DEBUG: " + o);
        }
        public static void LogWarning (object o) {
            if (log) Debug.LogWarning ("MY DEBUG: " + o);
        }
        public static void LogError (object o) {
            if (log) Debug.LogError ("MY DEBUG: " + o);
        }

        public bool showLog = true;
        private static bool log = true;
        void Awake () {
            log = showLog;
        }
    }
}
