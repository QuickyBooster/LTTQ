using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PointEnemy : MonoBehaviour
{
    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Sprite _iconSquare;
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

    private void OnMouseDown()
    {
        isBeingAttack();
    }

    bool isBeingAttack()
    {
        if (_destroyed) return false ;
        if (!_controller.isEnemyTurn())
        {
            if (_shipField)
            {

                GetComponent<SpriteRenderer>().sprite =_iconDestroyed;
                _destroyed = true;
                _controller.toggleEnemyTurn(true);
                _controller.returnPointHit(_id);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _iconSquare;
                _destroyed = true;
                //_controller.toggleEnemyTurn(false);
            }
        }

        return false;
    }
    public bool isDestroyed() { return _destroyed; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _shipField= true;
    }
    private void OnMouseOver()
    {
        isBeingAttack();
    }
}
