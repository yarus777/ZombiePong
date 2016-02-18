using UnityEngine;

namespace Assets.Scripts.Menu {
    using Effects;

    public class SoundToggle : MonoBehaviour {

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void MusicToogleChanged(bool isChecked)
        {
            SoundManager.Instance.IsMusic = isChecked;
            //MusicManager.Instance.IsMusic = isChecked;
            Debug.Log("Checked" + isChecked);

        }

        public void SoundToogleChanged(bool isChecked)
        {
            SoundManager.Instance.IsFx = isChecked;
            //MusicManager.Instance.IsSound = isChecked;
            Debug.Log("IsSoundChecked" + isChecked);

        }
    }
}
