using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    [SerializeField] Sprite soundOff;
    [SerializeField] Sprite soundOni;
    [SerializeField] Button soundButton;
    private bool muted = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else 
            Load();
        //UpdateButtonIcon();
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
        //UpdateButtonIcon();
    }

   /* private void UpdateButtonIcon()
    {
        print("update");
        if (muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
            //soundButton.GetComponent<SpriteRenderer>().sprite = soundOni;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
            //soundButton.GetComponent<SpriteRenderer>().sprite = soundOff;
        }
    }*/
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
   /* private void OnMouseDown()
    {
        muted = !muted;
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
        UpdateButtonIcon();
    }
    public void toggleMute()
    {
        OnMouseDown();
    }*/
}
