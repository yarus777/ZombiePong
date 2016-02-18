namespace Assets.Scripts {
    using System.Collections;
    using Game;
    using UnityEngine;
    using UnityEngine.UI;

    public class Sharing : MonoBehaviour {
        public Button shareButton;
        private string screenshotFilename = "ScreenShot.png";

        void Start() {
#if UNITY_IPHONE
		shareButton.onClick.AddListener(OniOSMediaSharingClick);
#endif

#if UNITY_ANDROID
            shareButton.onClick.AddListener(OnAndroidMediaSharingClick);
#endif
        }

        public void OnAndroidMediaSharingClick() {
#if UNITY_ANDROID
            Application.CaptureScreenshot(screenshotFilename);
            StartCoroutine(SaveAndShare(screenshotFilename));
#endif
        }

        public void OniOSMediaSharingClick() {
#if UNITY_IPHONE
		StartCoroutine(ShareProcess());
#endif
        }

        IEnumerator ShareProcess() {
            yield return new WaitForEndOfFrame();
#if UNITY_IPHONE
		Texture2D screenshot = new Texture2D (Screen.width, Screen.height);
		screenshot.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
		screenshot.Apply ();
		
		byte[] imgBytes = screenshot.EncodeToPNG();
		uint imgLength = (uint)imgBytes.Length;
		int score = PlayerPrefs.GetInt("HighestLevel", 0);
         int points = GameLogic2d.Instance.GetCurrentScore();
	    string pointsText;
	    if (points%10 == 1)
	    {
	        pointsText = Texts.GetText(WhatText.PointsText1);
	    }
        else if (points%10 == 2 || points%10 == 3 || points%10 == 4)
        {
            pointsText = Texts.GetText(WhatText.PointsText3);
        }
        else
        {
            pointsText = Texts.GetText(WhatText.PointsText2);
        }
		string shareMessage = string.Format(Texts.GetText(WhatText.ShareText) + " "+points.ToString()+" "+pointsText);
		string subjectTitle = "Score Sharing";
		ActivityView.Share(shareMessage, subjectTitle, imgBytes, imgLength);
#endif
        }

        IEnumerator SaveAndShare(string fileName) {
            yield return new WaitForEndOfFrame();
#if UNITY_ANDROID

            var path = Application.persistentDataPath + "/" + fileName;
            Debug.Log(path);
            var intentClass = new AndroidJavaClass("android.content.Intent");
            var intentObject = new AndroidJavaObject("android.content.Intent");

            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("setType", "image/*");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"),
                "High Score");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "High Score ");
            var points = GameLogic2d.Instance.GetCurrentScore();
            string pointsText;
            if (points%10 == 1) {
                pointsText = Texts.GetText(WhatText.PointsText1);
            } else if (points%10 == 2 || points%10 == 3 || points%10 == 4) {
                pointsText = Texts.GetText(WhatText.PointsText3);
            } else {
                pointsText = Texts.GetText(WhatText.PointsText2);
            }
            var shareMessage =
                string.Format(Texts.GetText(WhatText.ShareText) + " " + points + " " + pointsText +
                              "\nhttps://play.google.com/store/apps/details?id=com.funbaster.kittypong");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            var uriClass = new AndroidJavaClass("android.net.Uri");
            var fileClass = new AndroidJavaClass("java.io.File");

            var fileObject = new AndroidJavaObject("java.io.File", path); // Set Image Path Here

            var uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

            //			string uriPath =  uriObject.Call<string>("getPath");
            var fileExist = fileObject.Call<bool>("exists");
            Debug.Log("File exist : " + fileExist);
            if (fileExist) {
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
                    uriObject);
            }

            var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("startActivity", intentObject);
#endif
        }
    }
}