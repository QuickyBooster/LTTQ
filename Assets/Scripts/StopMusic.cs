using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopMusic : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PreBattle")
            MusicControl.instance.GetComponent<AudioSource>().Pause();
    }
}
