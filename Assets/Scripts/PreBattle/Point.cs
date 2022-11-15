using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{

    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Sprite _iconSquare;
    [SerializeField] Sprite _iconPoint;
    Controller _controller;

    SpriteRenderer _renderer;
    bool _shipField;
    bool _destroyed;
    int _id;

    private void Start()
    {
        _destroyed = false;
        int.TryParse(this.name, out _id);
    }
    private void Awake()
    {
        _controller = FindObjectOfType<Controller>();
        _renderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(this.gameObject);
    }
    
    public bool Destroyed() { return _destroyed; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _shipField= true;
        print("yes");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _shipField= true;
        print("yes");
    }

    public bool isBeingAttack()
    {
        if (!_destroyed)
        {
            if (_shipField)
            {

                _renderer.sprite =_iconDestroyed;
                _destroyed = true;
                //_controller.setEnemyTurn(false);
                return true;
            }
            else
            {
                _renderer.sprite = _iconSquare;
                _destroyed = true;
                //_controller.toggleEnemyTurn(false);
                return false;
            }
        }
        return false;
    }
    public void setShipField(bool status)
    {
        _shipField = status;    
    }
    public void resetAllElement()
    {
        _shipField = false;
        _destroyed = false;
        _renderer.sprite = _iconPoint;
    }
    //private void OnMouseOver()
    //{
    //    if (_destroyed) return;
    //    if (_controller.isEnemyTurn()) ;
    //    {
    //        if (_shipField)
    //        {

    //            _renderer.sprite =_iconDestroyed;
    //            _destroyed = true;
    //            //_controller.setEnemyTurn(false);
    //        }
    //        else
    //        {
    //            _renderer.sprite = _iconSquare;
    //            _destroyed = true;
    //            //_controller.setEnemyTurn(false);
    //        }
    //    }
    //}
}