using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    bool destroyed;
    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Controller _controller;

    private void Start()
    {
        destroyed = false;
    }
    private void Awake()
    {
        int id;
        string name = this.name;
        if (int.TryParse(name,out id))
        {
            int i = id / 21;
            int j = id % 21;
        }
        _controller = FindObjectOfType<Controller>();

    }
    private void OnMouseDown()
    {
        if (destroyed) return;
        Debug.Log(this.name+" chua bi destroy");
        if (_controller._turn)
        {
            GetComponent<SpriteRenderer>().sprite =_iconDestroyed;
        destroyed = true;
        _controller._turn = false;
        Debug.Log(this.name+" destroyed");
        }
    }
    private void OnMouseOver()
    {
        Debug.Log("you are over "+this.name); 
    }
}
