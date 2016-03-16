using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AdSDK;
using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets
{
    class RateUsPopup : MonoBehaviour
    {
        public Animator animator;
        public void RateUsBtnClick()
        {
            SoundManager.Instance.Play(FxType.MenuButton);
            PlayerPrefs.SetInt("rateus_clicked", 1);
            PlayerPrefs.Save();
            AdSDK.SendEvent("RATE_US_YES");
            Application.OpenURL("https://play.google.com/store/apps/details?id=zombie.smash.evil"); 
        }

        public void LaterBtnClick()
        {
            SoundManager.Instance.Play(FxType.MenuButton);
            AdSDK.SendEvent("RATE_US_LATER");
            Time.timeScale = 1;
            //animator.SetBool("isAnimated", true);
        }

        public void ExitBtnClick()
        {
            SoundManager.Instance.Play(FxType.MenuButton);
        }

        void Awake()
        {
            animator.SetBool("isAnimated", true);
            Debug.Log("RateUsClicked" + PlayerPrefs.GetInt("rateus_clicked", 0));
            int isClicked = PlayerPrefs.GetInt("rateus_clicked", 0);

            if (isClicked == 1)
            {
                gameObject.SetActive(false);
            }

            else  
            {
                int count = PlayerPrefs.GetInt("rateus_count", 0);
                count++;
                Debug.Log("RateUsCount" + PlayerPrefs.GetInt("rateus_count", 0));
                if (count == 5)
                {
                    gameObject.SetActive(true);
                    Time.timeScale = 0;
                    //animator.SetBool("isAnimated",false);
                    AdSDK.SendEvent("RATE_US_SHOW");
                    count = 0;
                }
                else
                {
                    gameObject.SetActive(false);
                }
                PlayerPrefs.SetInt("rateus_count", count);
                PlayerPrefs.Save();
            }
            
        }

    }
}
