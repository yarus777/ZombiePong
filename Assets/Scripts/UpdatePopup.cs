using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class UpdatePopup : MonoBehaviour
    {
       
        public const string PREV_SCENE_NAME_KEY = "prev_scene";
        void Awake()
        {
            gameObject.SetActive(!PlayerPrefs.HasKey(PREV_SCENE_NAME_KEY));
            PlayerPrefs.DeleteKey(PREV_SCENE_NAME_KEY);
            PlayerPrefs.Save();
        }

        public void UpdateBtnClick()
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=zombie.smash.evil");
        }
    }
}
