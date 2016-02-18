namespace Assets.Scripts.Tutorials {
    using System;
    using System.Collections.Generic;
    using Events;
    using Popups;
    using UnityEngine;

    class Tutorial : MonoBehaviour {
        private const string FIRST_START_KEY = "firstStart";

        [SerializeField]
        private TutorialItem[] _steps;

        private List<TutorialStep> _activeSteps;

        public event Action StepBecameActive;
        public event Action StepBecameInactive;

        public bool IsFirstRun { get; private set; }

        private void Awake() {
            IsFirstRun = PlayerPrefs.GetInt(FIRST_START_KEY, 0) == 0;
            Debug.Log("First: " + IsFirstRun);
            if (!IsFirstRun) {
                return;
            }
            PlayerPrefs.SetInt(FIRST_START_KEY, 1);
            _activeSteps = new List<TutorialStep>();
            foreach (var step in _steps) {
                var s = step;
                step.Event.Fired += () => OnStepFired(s.Step);
            }
        }

        private void OnStepFired(TutorialStep step) {
            Show(step);
        }

        private void Show(TutorialStep step) {
            _activeSteps.Add(step);
            step.Show(OnStepShown);
            UpdateShade();
            var handler = StepBecameActive;
            if (handler != null) {
                handler();
            }
        }

        private void OnStepShown(TutorialStep step) {
            step.gameObject.SetActive(false);
            _activeSteps.Remove(step);
            UpdateShade();
            if (step.AutoResume) {
                var handler = StepBecameInactive;
                if (handler != null) {
                    handler();
                }
            } else {
                PopupsController.Instance.Show(PopupType.GetReady);
            }
        }

        private void UpdateShade() {
        }

        [Serializable]
        private class TutorialItem {
            public TutorialStep Step;
            public TutorialEvent Event;
        }
    }
}