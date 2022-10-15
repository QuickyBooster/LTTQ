using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{
    bool destroyed;
    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Controller _controller;
    SpriteRenderer _renderer;

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
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (destroyed) return;
        if (_controller._turn)
        {
            GetComponent<SpriteRenderer>().sprite =_iconDestroyed;
            destroyed = true;
            _controller._turn = false;
        }
    }
    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().sprite =_iconDestroyed;
    }
}
