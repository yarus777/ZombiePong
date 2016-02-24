namespace Assets.Scripts.Tutorials {
    using System;
    using Effects;
    using UnityEngine;

    class TutorialStep : MonoBehaviour {
        private Action<TutorialStep> _onShowed;

        public void Show(Action<TutorialStep> onShowed) {
            gameObject.SetActive(true);
            _onShowed = onShowed;
        }

        public virtual bool AutoResume {
            get { return true; }
        }

        public void Complete() {
            SoundManager.Instance.Play(FxType.MenuButton);
            _onShowed(this);
        }
    }
}