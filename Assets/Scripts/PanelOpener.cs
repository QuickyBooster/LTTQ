using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;
    public bool isActive;
    public Transform cardTransform;
    void Start()
    {
        isActive = false;
        panel.SetActive(false);
    }

    public void Use()
    {
        if (isActive == true)
        {
            isActive = false;
            panel.SetActive(isActive);
        }
    }

    public void Save()
    {
        if (isActive == true)
        {
            isActive = false;
            panel.SetActive(isActive);
        }
    }

}
