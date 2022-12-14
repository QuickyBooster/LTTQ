using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;

   // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
            Load();
        UpdateStopIcon();
        AudioListener.pause = muted;
    }
    public void OnButtonPress()
    {
        print("press");
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateStopIcon();
    }

    private void UpdateStopIcon()
    {
        if (muted == false)
            soundOffIcon.enabled = false;
        else
            soundOffIcon.enabled = true;
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
    /*public void MuteHandle(bool mute)
     {
         if (mute)
             AudioListener.volume = 0;
         else
             AudioListener.volume = 1;
     }*/
}
