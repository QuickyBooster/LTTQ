using System.Collections;
using UnityEngine;

public class OpenScene : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;

    private void Awake()
    {
        animator1 = GameObject.Find("scene_left").GetComponent<Animator>();
        animator2 = GameObject.Find("scene_right").GetComponent<Animator>();
    }
    public void LoadTransition()
    {
        StartCoroutine(openSceneLeft());
        StartCoroutine(openSceneRight());
        Debug.Log("scene");
    }
 
    IEnumerator openSceneLeft()
    {
        animator1.SetTrigger("scene_left");
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator openSceneRight()
    {
        animator2.SetTrigger("scene_right");
        yield return new WaitForSeconds(1.0f);
    }

}
