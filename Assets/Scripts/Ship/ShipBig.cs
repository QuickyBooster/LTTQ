using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipBig : MonoBehaviour
{
    Controller _controller;

    float _x;
    float _y;
    int _shipID;
    bool _touched;

    float _deltaX, _deltaY;
    Vector2 _mousePosition;
    private void Awake()
    {
        _controller = FindObjectOfType<Controller>();
        _shipID = _controller.getShipID();
        _touched = false;
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void OnMouseDown()
    {
        if (!_controller.isLocked())
        {
            _deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            _deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            _controller.setID((_shipID-1000), -1, false);
        }
    }
    private void OnMouseDrag()
    {
        if (!_controller.isLocked())
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(_mousePosition.x - _deltaX, _mousePosition.y - _deltaY);
        }
    }
    private void OnMouseUp()
    {
        if (!_controller.isLocked())
        {

            // find the standard position of it
            int id = 0;
            float vX, vY, tX, tY;
            vX = transform.position.x -2.4445f;
            vY = transform.position.y +0.3745f;
            tX = -8.216f;
            for (int i = 0; i < 8; i++)
            {
                tY = 2.739f;
                for (int j = 0; j < 18; j++)
                {
                    if (Mathf.Abs(vX-tX)<=0.2f && Mathf.Abs(vY-tY)<=0.2f)
                    {
                        transform.position = new Vector2(tX+2.4445f, tY-0.3745f);
                        _controller.setID(_shipID-1000, id, true);
                        if (_touched)
                        {
                            _controller.setID(_shipID-1000, -1, false);
                        }
                        return;
                    }
                    id++;
                    tY-= 0.3792f;
                }
                id++;
                tX+= 0.37825f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _touched = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _touched = false;
    }
}
