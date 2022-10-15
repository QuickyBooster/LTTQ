using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{

    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Sprite _iconSquare;
    Controller _controller;

    SpriteRenderer _renderer;
    bool _shipField;
    bool _destroyed;

    private void Start()
    {
        _destroyed = false;
    }
    private void Awake()
    {
        int id;
        string name = this.name;
        if (int.TryParse(name, out id))
        {
            int i = id / 21;
            int j = id % 21;
        }
        _controller = FindObjectOfType<Controller>();
        _renderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void OnMouseDown()
    {
        if (_destroyed) return;
        if (_controller.isEnemyTurn())
        {
            if (_shipField)
            {

                GetComponent<SpriteRenderer>().sprite =_iconDestroyed;
                _destroyed = true;
                //_controller.setEnemyTurn(false);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _iconSquare;
                _destroyed = true;
                //_controller.setEnemyTurn(false);
            }
        }
    }
    public bool isDestroyed() { return _destroyed; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _shipField= true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _shipField = false;
    }
    private void OnMouseOver()
    {
        if (_destroyed) return;
        if (_controller.isEnemyTurn())
        {
            if (_shipField)
            {

                GetComponent<SpriteRenderer>().sprite =_iconDestroyed;
                _destroyed = true;
                //_controller.setEnemyTurn(false);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _iconSquare;
                _destroyed = true;
                //_controller.setEnemyTurn(false);
            }
        }
    }
}
