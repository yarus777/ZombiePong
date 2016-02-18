namespace Assets.Scripts.Popups {
    using Game;

    class GetReadyPopup : Popup {
        public override void Close() {
            base.Close();
            GameLogic2d.Instance.ResumeGame();
        }
    }
}