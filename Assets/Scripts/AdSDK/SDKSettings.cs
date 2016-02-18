using UnityEngine;

namespace Assets.Scripts.AdSDK {
    using System.Collections;

    public class SDKSettings : MonoBehaviour {
        public string publisherId = "appliciada";
        public string appKey = "mahjong" ;
        public string affId = "appliciada" ;
	
        public string market = "googleplay";
	
        public string placementRKey = "r_game";
        public string placementIRKey = "ir_game";
        public string placementFKey = "f_game";
	
        public string flurryKey = "";
	
        public bool debug = true;
#if UNITY_ANDROID
	public AdSDK.BannerAlignment alignment = AdSDK.BannerAlignment.BOTTOM;
	
	static bool once = false;
	
	void Awake () {
		if (once) {
			Destroy (this.gameObject);
		} else {
			DontDestroyOnLoad (this.gameObject);
			once = true;
			InitSDK ();
			AdSDK.StartSDK (debug);
			AdSDK.StartFlurrySession ();
		}
	}
	
	IEnumerator Start () {
		yield return new WaitForSeconds (1);
		AdSDK.setVideoListener ();
		yield return new WaitForSeconds (1);
		AdSDK.PreloadVideo ();
		yield return new WaitForSeconds (1);
		AdSDK.TryShowFullscreen ();
		Destroy (this.gameObject);
	}
	void InitSDK()
	{
		AdSDK.publisherId = publisherId;
		AdSDK.appKey = appKey;
		AdSDK.affId = affId;
		AdSDK.placementRKey = placementRKey;
		AdSDK.placementIRKey = placementIRKey;
		AdSDK.placementFKey = placementFKey;
		AdSDK.flurryKey = flurryKey;
		AdSDK.alignment = alignment;
		AdSDK.market = market;	
		AdSDK.bannerWidth = 320;
		AdSDK.bannerHeight = 50;
	}
	#endif
    }
}
