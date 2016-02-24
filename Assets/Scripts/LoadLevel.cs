﻿using Assets.Scripts.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class LoadLevel : MonoBehaviour
    {
        public Button playBtn;

        // Use this for initialization
        public void LoadingLevel (string level)
        {           
            SoundManager.Instance.Play(FxType.MenuButton);
            Application.LoadLevelAsync (level);
            playBtn.interactable = false;
        }
    }
}
