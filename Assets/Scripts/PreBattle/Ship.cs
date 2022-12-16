using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Ship : MonoBehaviour
{
    [SerializeField] UIManager _manager;
    Controller _controller;

    float _x;
    float _y;

    float _deltaX, _deltaY;
    Vector2 _mousePosition;

    bool _lockedShipCoordinate;

    private void Start()
    {
        _lockedShipCoordinate = false;
        _manager = FindObjectOfType<UIManager>();
    }
    private void Awake()
    {
        _controller = FindObjectOfType<Controller>();
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnMouseDown()
    {
        if (!isLocked())
        {
            _deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            _deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            _controller.setShipInPlace(false, 0);

        }
    }
    private void OnMouseDrag()
    {
        if (!isLocked())
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(_mousePosition.x - _deltaX, _mousePosition.y - _deltaY);
        }
    }
    private void OnMouseUp()
    {
        if (!isLocked())
        {

            // find the standard position of it
            int id = 0;
            float vX, vY, tX, tY;
            vX = transform.position.x - 0.7224f;
            vY = transform.position.y;
            tX = -8.3325f;
            for (int i = 0; i < 3; i++)
            {
                tY = 0.172f;
                for (int j = 0; j < 5; j++)
                {
                    if (Mathf.Abs(vX - tX) <= 0.35f && Mathf.Abs(vY - tY) <= 0.35f)
                    {
                        transform.position = new Vector2(tX + 0.7224f, tY);
                        _controller.setShipInPlace(true, i * 5 + j);
                        return;
                    }
                    id++;
                    tY -= 0.7197f;
                }
                id++;
                tX += 0.7198f;
            }
        }
    }
    public void toggleCollider()
    {
        GetComponent<Collider2D>().enabled = !GetComponent<Collider2D>().enabled;
    }
    public void exitGame()
    {
        Destroy(this.gameObject);
    }

    public bool isLocked() { return _lockedShipCoordinate; }
    public void setLockedCoordinate(bool set)
    {
        _lockedShipCoordinate = set;
    }
    public bool isLockedCoordinate()
    {
        return _lockedShipCoordinate;
    }

    void checkReady()
    {
        if (_lockedShipCoordinate && _controller.isFinishedChoosingCard())
            _manager.showButtonBattle(true);
        else
            _manager = FindObjectOfType<UIManager>();
    }
}
