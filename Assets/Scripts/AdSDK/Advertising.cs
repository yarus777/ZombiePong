using UnityEngine;

#if UNITY_IOS
//using AppodealAds.Unity.Api;
//using UnityEngine;
#endif
namespace Assets.Scripts.AdSDK {
    public static class Advertising{

        // Use this for initialization

        public static void ShowFullScreen(){
#if UNITY_ANDROID
		AdSDK.TryShowFullscreen ();
#endif
#if UNITY_IOS
    //Appodeal.show(Appodeal.INTERSTITIAL);
#endif
#if UNITY_EDITOR
            Debug.Log("Show Fullscreen");
#endif
        }

        public static void ShowBanner(){}
    }
}
