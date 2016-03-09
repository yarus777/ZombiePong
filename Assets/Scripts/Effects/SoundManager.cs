namespace Assets.Scripts.Effects {
    #region References

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    #endregion

    internal enum FxType {
        MenuButton,
        BallHit,
        GameOver
    }

    class SoundManager : UnitySingleton<SoundManager> {
        private string FX_KEY = "fx";
        private string MUSIC_KEY = "music";

        #region Visible in inspector

        [SerializeField]
        private FxItem[] _fx;

        [SerializeField]
        private AudioSource _backgroundMusic;

        #endregion

        private bool _isMusic;
        private bool _isFx;

        private IDictionary<FxType, AudioSource> _fxMapping;

        public bool IsMusic {
            get { return _isMusic; }
            set {
                _isMusic = value;
                _backgroundMusic.mute = !value;
                SaveMusic();
            }
        }

        public bool IsFx {
            get { return _isFx; }
            set {
                _isFx = value;
                SaveFX();
            }
        }

        public void Play(FxType fxType) {
            if (!_fxMapping.ContainsKey(fxType)) {
                return;
            }
            var fx = _fxMapping[fxType];
            fx.mute = !IsFx;
            fx.Play();
        }

        private void Load() {
            IsFx = PlayerPrefs.GetInt(FX_KEY, 1) == 1;
            IsMusic = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
        }

        private void SaveFX() {
            PlayerPrefs.SetInt(FX_KEY, IsFx ? 1 : 0);
        }

        private void SaveMusic() {
            PlayerPrefs.SetInt(MUSIC_KEY, IsMusic ? 1 : 0);
        }

        protected override void LateAwake() {
            base.LateAwake();
            _fxMapping = _fx.ToDictionary(x => x.Type, x => x.Source);
            Load();
            _backgroundMusic.Play();
        }

        // класс для лучшей структуризации в инспекторе Unity
        [Serializable]
        internal class FxItem {
            public FxType Type;
            public AudioSource Source;
        }
    }
}