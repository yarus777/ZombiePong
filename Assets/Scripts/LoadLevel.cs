using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets.Scripts {
    public class LoadLevel : MonoBehaviour {

        // Use this for initialization
        public void LoadingLevel (string level) {
            SoundManager.Instance.Play(FxType.MenuButton);
            Application.LoadLevelAsync (level);
        }
    }
}
