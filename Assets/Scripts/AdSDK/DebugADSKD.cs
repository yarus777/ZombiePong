using UnityEngine;
using System.Collections;
using Assets.Scripts.AdSDK;

public class DebugADSKD : MonoBehaviour {
	public static DebugADSKD inst;
	void Start()
	{
		inst = this;
		DontDestroyOnLoad (gameObject);
	}
#if UNITY_ANDROID
	public void ListenForShow () {
		StartCoroutine (Listen ());
	}

	IEnumerator Listen () {
		while (PlayerPrefs.GetInt ("VIDEO_SHOW_OK") == 0) {
			
			yield return new WaitForSeconds (1f);
		}
		AdSDK.onDismissVideo ();
		PlayerPrefs.SetInt ("VIDEO_SHOW_OK", 0);
	}
#endif
}