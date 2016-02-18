namespace Assets.Scripts.AdSDK {
    using UnityEngine;

    public static class AdSDK {
#if UNITY_ANDROID

        public enum GenderType {
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

        public enum BannerAlignment {
            TOP = 0,
            BOTTOM = 1
        }

        public static BannerAlignment alignment;

        public static int bannerWidth;
        public static int bannerHeight;
        private static bool bannerActive;

#if !UNITY_EDITOR
	private static bool isFirstLaunch = true;
	#endif

        private static void Init(bool debug) {
            MyDebug.Log("Init SDK");
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("initialize", getCurrentActivity(), debug, flurryKey);
		#endif
        }

        public static void PreloadVideo() {
            MyDebug.Log("PreloadVideo start");
            stateVideo = State.Loading;
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("preloadVideo", getCurrentActivity());
		#else
            stateVideo = State.LoadingFailed;
#endif
        }

        public static void ShowVideo() {
            MyDebug.Log("ShowVideo start");
            stateVideo = State.Idle;
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("showVideo", getCurrentActivity());
		#endif
            PreloadVideo();
        }

        public static void setVideoListener() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("setVideoScreenListener", new VideoListener());
		#endif
        }

        /*
	public static void PreloadFullscreen() {
		MyDebug.Log ("MYDEBUG: PreloadFullscreen start");
		stateFullScreen = State.Loading;
		#if !UNITY_EDITOR
		AdSDK.setFullscreenListener (fullscreenListener);
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("preloadFullscreen", getCurrentActivity());
		#else
		stateFullScreen = State.LoadingFailed;
		#endif
	}
	*/

        public static void TryShowFullscreen() {
            ShowFullscreen();
            /*
		MyDebug.Log ("MYDEBUG: AdSDK.stateFullScreen = " + AdSDK.stateFullScreen);
		switch (AdSDK.stateFullScreen) {
		case AdSDK.State.Ready:
			ShowFullscreen ();
			PreloadFullscreen();
			break;
		default:
			PreloadFullscreen ();
			break;
		}*/
        }

        public static void ShowFullscreen() {
            MyDebug.Log("ShowFullscreen start");
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("showFullScreenOrPreload", getCurrentActivity());
		#endif
        }

        /*
	public static void setFullscreenListenerIfHavenot(FullscreenListener fsl) {
		#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("setFullscreenListenerIfHavenot", fsl);
		#endif
	}*/
/*
	public static void setFullscreenListener(FullscreenListener fsl) {
		#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("setFullScreenListener", getCurrentActivity(), fsl);
		#endif
	}
	*/

        public static void StartDebug() {
#if !UNITY_EDITOR
		AndroidJavaObject param = BuildBannerParams(placementFKey);  
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("startDebug", param, getCurrentActivity());
		#endif
        }


        public static void StopDebug() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("stopDebug", getCurrentActivity());    
		#endif
        }

        public static void DestroyBanner() {
            bannerActive = false;
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("destroy", getCurrentActivity());
		#endif
        }


        private static AndroidJavaObject BuildBannerParams(string placement) {
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


        private static AndroidJavaObject GetParams() {
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

        public static void CreateBanner() {
            bannerActive = true;
#if !UNITY_EDITOR
		AndroidJavaObject param = BuildBannerParams(placementRKey);
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("addBanner", param, (int) alignment, getCurrentActivity());
		#endif
        }

        public static void SetBannerVisible(bool visible) {
            if (visible && !bannerActive) {
                CreateBanner();
            }
            if (!visible && bannerActive) {
                DestroyBanner();
            }
            //#if !UNITY_EDITOR
            //		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
            //		clazz.CallStatic("setBannerVisible", visible, getCurrentActivity());
            //#/endif
        }

        private static AndroidJavaObject getCurrentActivity() {
#if !UNITY_EDITOR
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = ajc.GetStatic<AndroidJavaObject>("currentActivity");
		return activity;
		#else
            return null;
#endif
        }

        // Analytics methods

        public static void StartFlurrySession() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		AndroidJavaObject param = BuildBannerParams(placementVRKey);
		clazz.CallStatic("onStart", getCurrentActivity(), flurryKey, param);
		#endif
        }

        public static void StopFlurrySession() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("onStop", getCurrentActivity());
		#endif
        }

        private static void SendDownload() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendDownload", getCurrentActivity(), publisherId, affId, appKey);
		#endif
        }

        public static void SendConversion() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendConversion", getCurrentActivity(), publisherId, affId, appKey);
		#endif
        }

        public static void SendEvent(string someEvent) {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendCustomEvent", getCurrentActivity(), publisherId, affId, appKey, someEvent);
		#endif
        }

        public static void listenForInstall(string packageName, string eventName) {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("listenForInstall", getCurrentActivity(), packageName, eventName, publisherId, affId, appKey);
		#endif
        }

        public static void StartSDK(bool debug = true) {
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

        public enum State {
            LoadingFailed,
            Loading,
            Ready,
            Idle
        }

        /*
	public static State stateFullScreen = State.Idle;

	public class FullscreenListener : AndroidJavaProxy {

//
//        void onAdClick(Interstitial ad);
//
//        void onDismiss(Interstitial ad);
//
//		void onAdLoaded(Interstitial ad);
//
//        void onAdLoadFailed(Interstitial ad, ErrorCode errorCode);
//
//        void onAdReady(Interstitial ad);
//
//        void onAdReadyFailed(Interstitial ad, ErrorCode errorCode);
//

		public FullscreenListener() : base("com.inappertising.ads.Interstitial$InterstitialUnityListener") { 
			MyDebug.Log("FullscreenListener created");
		}
		void onAdClick(){
			MyDebug.Log("FullscreenListener onAdClick");
		}
		void onDismiss(){
			MyDebug.Log("FullscreenListener onDismiss");
		}
		void onAdLoaded(){
			MyDebug.Log("ShowFullscreen onAdLoad");
		}
		void onAdLoadFailed(){
			MyDebug.Log("ShowFullscreen onAdLoadFailed");
		}
		void onAdReady(){
			MyDebug.Log("ShowFullscreen onAdReady");
			AdSDK.stateFullScreen = State.Ready;
		}
		void onAdReadyFailed(){
			MyDebug.Log("ShowFullscreen onAdReadyFailed");
			AdSDK.stateFullScreen = State.LoadingFailed;
		}
	}
	*/
        public static State stateVideo = State.Idle;
        public static int count = 2;
        public static bool forWatch = false;
        public static bool forDouble = false;
        /*
#if !UNITY_EDITOR
	public static FullscreenListener fullscreenListener {
		get {
			if (_fullLis == null) {
				_fullLis = new FullscreenListener ();
			}
			return _fullLis;
		}
	}
	static FullscreenListener _fullLis;
#endif
*/

        class VideoListener : AndroidJavaProxy {
            public VideoListener() : base("com.inappertising.ads.Video$VideoAdListener") {
                MyDebug.Log("VideoListener created");
                count = 2;
            }

            void onAdFailedToLoad(System.String reason) {
                if (count > 0) {
                    MyDebug.Log("Preload video after failing (" + count + ")");
                    PreloadVideo();
                    count --;
                } else {
                    stateVideo = State.LoadingFailed;
                }
                MyDebug.Log("ShowVideo onAdLoadFailed " + reason);
            }

            void onAdLoaded() {
                count = 2;
                stateVideo = State.Ready;
                MyDebug.Log("ShowVideo onAdLoaded");
            }

            void onDismiss() {
//			if (forDouble) {			
//				InfoTopManager.inst.ShowMessage (Texts.GetText (WhatText.CongrYouHaveDouble));
//				forDouble = false;
//				MahjongManager.staticWinPopap.money.text = "+" + (2*MahjongManager.moneyWin);
//				SoomlaManager.AddMoney (MahjongManager.moneyWin);
//				SendEvent ("FREE_COINS_DOUBLE_VIDEO");
//			}
//			if (forWatch) {
//				SoomlaManager.AddFreeMoney (PlayerInfo.Shop.FREE_VIDEO);
//				InfoTopManager.inst.ShowMessage ("+" + PlayerInfo.Shop.FREE_VIDEO + Texts.GetText (WhatText.CoinsTakeForVideoInfo));
//				forWatch = false;
//			}
                MyDebug.Log("ShowVideo onDismiss");
            }
        }
#endif
    }
}