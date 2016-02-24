using Assets.Scripts.Effects;

namespace Assets.Scripts.Popups {
    using Game;

    class SettingsPopup : Popup {
        public override void Close() {
            SoundManager.Instance.Play(FxType.MenuButton);
            base.Close();
            if (GameLogic2d.Instance != null) {
                PopupsController.Instance.Show(PopupType.GetReady);
            }
        }
    }
}