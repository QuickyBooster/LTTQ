using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSmall : MonoBehaviour
{
    float x;
    float y;

    Transform shipBigPlace;
    float deltaX, deltaY;
    Vector2 mousePosition, initialPosition;
    bool locked;
    private void Start()
    {
        initialPosition = transform.position;
    }
    private void OnMouseDown()
    {
        if (!locked)
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }
    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition =Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
    }
}
