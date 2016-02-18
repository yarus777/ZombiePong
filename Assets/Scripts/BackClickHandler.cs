using UnityEngine;

namespace Assets.Scripts {
    public class ButtonManager : MonoBehaviour {

        void Update ()
        {
            if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.Backspace)))
            {
                OnBackPressed();
            }
        }
	
        public void OnBackPressed()
        {
            /*if (!settingsPanel.activeSelf)
		{
			settingsPanel.SetActive(false);
		}
		else
			Application.Quit();*/
        }
    }
}
