using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Animator room;
    private void Awake()
    {
        room = GameObject.Find("Room").GetComponent<Animator>();
    }
    public void openRoom()
    {
        Debug.Log("open");
        StartCoroutine(roomOn());
    }
    public void closeRoom()
    {
        StartCoroutine(roomOff());
        Debug.Log("close");
    }
    IEnumerator roomOn()
    {
        room.SetTrigger("roomOn");
        yield return new WaitForSeconds(.5f);
    }
    IEnumerator roomOff()
    {
        room.SetTrigger("roomOff");
        yield return new WaitForSeconds(.5f);
    }
}
