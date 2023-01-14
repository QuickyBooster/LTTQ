using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(roomOn());
    }
    IEnumerator roomOn()
    {
        room.SetTrigger("roomOn");
        yield return new WaitForSeconds(.5f);
    }
}
