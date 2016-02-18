using UnityEngine;

namespace Assets.Scripts.AdSDK {
    public class DebugADSKD : MonoBehaviour {
        public static DebugADSKD inst;
        void Start()
        {
            inst = this;
            DontDestroyOnLoad (gameObject);
        }

#if UNITY_ANDROID
	void OnApplicationPause(bool pauseStatus) {
		if(pauseStatus)
		{
			AdSDK.StartDebug();
		}
		else
		{
			AdSDK.StopDebug();
		}
	}
#endif

    }
}
