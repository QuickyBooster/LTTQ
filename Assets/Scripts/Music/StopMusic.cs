using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopMusic : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "PreBattle")
            MusicControl.instance.GetComponent<AudioSource>().Pause();
    }

    public static StopMusic instance1;
    private void Awake()
    {
        if (instance1 != null)
            Destroy(gameObject);
        else
        {
            instance1 = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
