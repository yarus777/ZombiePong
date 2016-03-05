using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AdSDK {

    public static class AdSDK
    {
#if UNITY_ANDROID

        public enum GenderType
        {
            UNKNOWN,
            MALE,
            FEMALE
        }

        //	private static AndroidJavaObject banner = null;
        //	private static AndroidJavaObject debugBanner = null;

        public static string publisherId = "adeco";
        public static string appKey = "barleybreak";
        public static string affId = "adeco";

        public static string market = "4shared.com";

        public static string placementRKey = "r_game";
        public static string placementVRKey = "vr_game";
        public static string placementIRKey = "ir_game";
        public static string placementFKey = "f_game";

        public static string flurryKey = "";

        public enum BannerAlignment
        {
            TOP = 0,
            BOTTOM = 1
        }

        public static BannerAlignment alignment;

        public static int bannerWidth;
        public static int bannerHeight;
        private static bool bannerActive = false;

#if !UNITY_EDITOR
	private static bool isFirstLaunch = true;
	#endif

        private static void Init(bool debug)
        {

//		Debug.Log ("MYDEBUG: External storage path = " + PlayerPrefs.GetString ("EXTERNAL_STORAGE_PATH"));
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("initialize", getCurrentActivity(), debug, flurryKey);
		#endif
        }

        public static void Finish()
        {

#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.jm.Mahjong.SDKProxy");
		clazz.CallStatic("finish", getCurrentActivity());
		#endif
        }

//	static float TIME = 0;
        public static void ShowOrPreloadVideo()
        {

#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.jm.Mahjong.SDKProxy");
		clazz.CallStatic("ShowOrPreload", getCurrentActivity());
		#endif
        }

        public static void PreloadVideo()
        {

#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.jm.Mahjong.SDKProxy");
		clazz.CallStatic("Preload", getCurrentActivity());
		#endif
        }

        public static void ShowFullscreen()
        {

#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("showFullScreenOrPreload", getCurrentActivity());
		#endif
        }

        public static void StartDebug()
        {
#if !UNITY_EDITOR
		AndroidJavaObject param = BuildBannerParams(placementFKey);  
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("startDebug", param, getCurrentActivity());
		#endif
        }


        public static void StopDebug()
        {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("stopDebug", getCurrentActivity());    
		#endif
        }

        public static void DestroyBanner()
        {
            bannerActive = false;
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("destroy", getCurrentActivity());
		#endif
        }


        private static AndroidJavaObject BuildBannerParams(string placement)
        {
#if !UNITY_EDITOR
		AndroidJavaObject obj = GetParams();
		obj.Call<AndroidJavaObject>("setPlacementKey", placement);
		AndroidJavaObject obj2 = new AndroidJavaObject ("com.inappertising.ads.ad.AdSize", bannerWidth, bannerHeight);
		obj.Call<AndroidJavaObject> ("setSize", obj2);
		AndroidJavaObject param = obj.Call<AndroidJavaObject>("build");
		return param;
		#else
            return null;
#endif
        }


        private static AndroidJavaObject GetParams()
        {
#if !UNITY_EDITOR
		AndroidJavaObject obj = new AndroidJavaObject("com.inappertising.ads.ad.AdParametersBuilder");
		obj.Call<AndroidJavaObject>("setPublisherId", publisherId);
		obj.Call<AndroidJavaObject>("setAppKey", appKey);
		
		obj.Call<AndroidJavaObject>("setMarket", market);
		
		obj.Call<AndroidJavaObject>("setAffId", affId);
		return obj;
		#else
            return null;
#endif
        }

        public static void CreateBanner()
        {
            bannerActive = true;
#if !UNITY_EDITOR
		AndroidJavaObject param = BuildBannerParams(placementRKey);
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("addBanner", param, (int) alignment, getCurrentActivity());
		#endif
        }

        public static void SetBannerVisible(bool visible, bool top = false, int width = 320, int height = 50)
        {
            if (visible)
            {
                if (bannerActive)
                {
                    if (top != (alignment == BannerAlignment.TOP))
                    {
                        DestroyBanner();
                        bannerWidth = width;
                        bannerHeight = height;
                        if (top)
                        {
                            alignment = BannerAlignment.TOP;
                        }
                        else
                        {
                            alignment = BannerAlignment.BOTTOM;
                        }
                        CreateBanner();
                    }
                }
                else
                {
                    bannerWidth = width;
                    bannerHeight = height;
                    if (top)
                    {
                        alignment = BannerAlignment.TOP;
                    }
                    else
                    {
                        alignment = BannerAlignment.BOTTOM;
                    }
                    CreateBanner();
                }
            }
            if (!visible && bannerActive)
            {
                DestroyBanner();
            }
            //#if !UNITY_EDITOR
            //		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
            //		clazz.CallStatic("setBannerVisible", visible, getCurrentActivity());
            //#/endif
        }

        private static AndroidJavaObject getCurrentActivity()
        {
#if !UNITY_EDITOR
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = ajc.GetStatic<AndroidJavaObject>("currentActivity");
		return activity;
		#else
            return null;
#endif
        }

        // Analytics methods

        public static void StartFlurrySession()
        {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		AndroidJavaObject param = BuildBannerParams(placementVRKey);
		clazz.CallStatic("onStart", getCurrentActivity(), flurryKey, param);
		#endif
        }

        public static void StopFlurrySession()
        {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("onStop", getCurrentActivity());
		#endif
        }

        private static void SendDownload()
        {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendDownload", getCurrentActivity(), publisherId, affId, appKey);
		#endif
        }

        public static void SendConversion()
        {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendConversion", getCurrentActivity(), publisherId, affId, appKey);
		#endif
        }

        public static void SendEvent(string someEvent)
        {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendCustomEvent", getCurrentActivity(), publisherId, affId, appKey, someEvent);
		#endif
        }

        public static void listenForInstall(string packageName, string eventName)
        {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("listenForInstall", getCurrentActivity(), packageName, eventName, publisherId, affId, appKey);
		#endif
        }

        public static void StartSDK(bool debug = true)
        {
#if !UNITY_EDITOR
		if (isFirstLaunch) {
			Init (debug);
			StartFlurrySession();
			if(!PlayerPrefs.HasKey("startApp"))
			{
				SendDownload();
				PlayerPrefs.SetInt("startApp", 1);
			}
			isFirstLaunch = false;
		}
		#endif
        }

        public enum State
        {
            LoadingFailed,
            Loading,
            Ready,
            Idle
        }

        public static State stateVideo
        {
            get
            {
                int val = PlayerPrefs.GetInt("VIDEO_STATE");
                if (val == 1) return State.Loading;
                if (val == 2) return State.LoadingFailed;
                if (val == 3) return State.Ready;
                return State.Idle;
            }
        }

        public static int count = 1;
        public static bool forWatch = false;
        public static bool forDouble = false;

        public static void onDismissVideo()
        {

        }
#endif
    }
}