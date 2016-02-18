namespace Assets.Scripts.Tutorials.Events {
    using System;
    using UnityEngine;

    internal abstract class TutorialEvent : MonoBehaviour {
        public event Action Fired;

        protected void OnFired() {
            var handler = Fired;
            if (handler != null) {
                handler();
            }
        }
    }
}