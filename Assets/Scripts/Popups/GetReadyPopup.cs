namespace Assets.Scripts.Popups {
    using Game;

    class GetReadyPopup : Popup {
        public override void Close() {
            base.Close();
            if (IsGameJustStarted) {
                GameLogic2d.Instance.StartPlaying();
            } else {
                GameLogic2d.Instance.ResumeGame();
            }
        }

        public override void OnBackClick() {
        }

        public override void OnShow() {
            base.OnShow();
            IsGameJustStarted = false;
        }

        public bool IsGameJustStarted { get; set; }
    }
}