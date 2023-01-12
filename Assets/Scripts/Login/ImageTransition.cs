using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImageTransition : MonoBehaviour
{
    public Animator image;
    private void Awake()
    {
        image = GameObject.Find("Image panel").GetComponent<Animator>();
    }
    public void image_login()
    {
        StartCoroutine(imageLogin());
    }
    public void image_register()
    {
        StartCoroutine(imageRegister());
    }
    IEnumerator imageLogin()
    {
        image.SetTrigger("image1");
        yield return new WaitForSeconds(.5f);
    }
    IEnumerator imageRegister()
    {
        image.SetTrigger("image2");
        yield return new WaitForSeconds(.5f);
    }
}
