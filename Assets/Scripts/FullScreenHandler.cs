namespace Assets.Scripts
{
    using AdSDK;
    using UnityEngine;

    class FullScreenHandler
    {
        public static void ShowAds()
        {
            int count = PlayerPrefs.GetInt("lose_count", 0);       
            count++;
            Debug.Log("LoseCount" + PlayerPrefs.GetInt("lose_count", 0));
			
            if (count == 3)
            {
				Advertising.ShowFullScreen();
                count = 0;
            }
            PlayerPrefs.SetInt("lose_count", count);
            PlayerPrefs.Save();
        }
    }


}
