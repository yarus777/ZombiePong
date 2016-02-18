using UnityEngine;
using System.Collections;
using Assets.Scripts.Effects;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{

    public Toggle soundToggle;
    public Toggle musicToggle;

	// Use this for initialization
	void Start () {
        musicToggle.isOn = PlayerPrefs.GetInt("music", 1) == 1;
        soundToggle.isOn = PlayerPrefs.GetInt("fx", 1) == 1;
        	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MusicToogleChanged(bool isChecked)
    {
        SoundManager.Instance.IsMusic = isChecked;
        
        Debug.Log("IsMusicChecked" + isChecked);

    }

    public void SoundToogleChanged(bool isChecked)
    {
        SoundManager.Instance.IsFx = isChecked;        
        Debug.Log("IsSoundChecked" + isChecked);

    }

    public void BtnPressed()
    {
        SoundManager.Instance.Play(FxType.MenuButton);
    }
}
