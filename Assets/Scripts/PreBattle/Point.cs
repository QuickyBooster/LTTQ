using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{

    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Sprite _iconSquare;
    [SerializeField] Sprite _iconPoint;
    [SerializeField] Sprite _iconBarrier;
    [SerializeField] Sprite _iconTorpedo;
    Controller _controller;

    SpriteRenderer _renderer;
    bool _shipField;
    bool _destroyed;
    int _id;
    bool _barrier;
    bool _torpedo;

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
                return true;
            }
            else
            {
                _renderer.sprite = _iconSquare;
                _destroyed = true;
                return false;
            }
        }
        return false;
    }
    public void setShipField(bool status)
    {
        _shipField = status;    
    }
    public bool isShipField()
    {
        return _shipField;
    }
    public void resetAllElement()
    {
        _destroyed = false;
        _renderer.sprite = _iconPoint;
    }

    IEnumerator displayTorpedo()
    {
        _renderer.sprite = _iconTorpedo;
        yield return new WaitForSeconds(1f);
    }
    public void setTorpedo(bool state)
    {
        _torpedo = state;
        if (!state)
        {
            StartCoroutine( displayTorpedo());
        }
        _renderer.sprite = _iconPoint;
    }
    public bool isTorpedo()
    {
        return (_torpedo);
    }
}
