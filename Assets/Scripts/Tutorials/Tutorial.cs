namespace Assets.Scripts.Tutorials {
    using System;
    using System.Collections.Generic;
    using Events;
    using Popups;
    using UnityEngine;

    class Tutorial : MonoBehaviour {
        public const string FIRST_START_KEY = "firstStart";

        [SerializeField]
        private TutorialItem[] _steps;

        [SerializeField]
        private GameObject _shade;

        private List<TutorialStep> _activeSteps;

        public event Action StepBecameActive;
        public event Action StepBecameInactive;

        public bool IsFirstRun {
            get { return PlayerPrefs.GetInt(FIRST_START_KEY, 0) == 0; }
        }

        private Queue<TutorialItem> _stepsQueue;

        private void Awake() {
            Debug.Log("First: " + IsFirstRun);
            if (!IsFirstRun) {
                return;
            }

            _stepsQueue = new Queue<TutorialItem>(_steps);
            _activeSteps = new List<TutorialStep>();
            var firstStep = _stepsQueue.Dequeue();
            firstStep.Event.Fired += () => OnStepFired(firstStep);
        }

        private void OnStepFired(TutorialItem step) {
            Show(step.Step);
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
            if (_stepsQueue.Count > 0) {
                var nextStep = _stepsQueue.Dequeue();
                nextStep.Event.Fired += () => OnStepFired(nextStep);
            } else {
                UpdateShade();
            }
        }

        private void UpdateShade() {
            _shade.SetActive(_activeSteps.Count > 0);
        }

        [Serializable]
        private class TutorialItem {
            public TutorialStep Step;
            public TutorialEvent Event;
        }
    }
}