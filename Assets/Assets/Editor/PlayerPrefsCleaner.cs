using UnityEditor;

using UnityEngine;

namespace Assets.Editor {
    internal class PlayerPrefsCleaner {
        [MenuItem("GameTools/Clear progress")]
        internal static void ClearProgress() {
            PlayerPrefs.DeleteAll();
        }
    }
}
