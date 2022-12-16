using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GameObject.Find("backgr").GetComponent<Animator>();
    }
    public void LoadTransition()
    {
        StartCoroutine(wipeIn());
        //StartCoroutine(wipeOut());
    }
    IEnumerator wipeIn()
    {
        animator.SetTrigger("wipeIn");
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator wipeOut()
    {
        animator.SetTrigger("wipeOut");
        yield return new WaitForSeconds(1.0f);
    }
}
