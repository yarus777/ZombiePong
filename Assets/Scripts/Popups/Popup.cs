namespace Assets.Scripts.Popups {
    using UnityEngine;

    class Popup : MonoBehaviour {
        public virtual void Close() {
            PopupsController.Instance.Close();
        }

        public virtual void OnShow() {

        }
    }
}