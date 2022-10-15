using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipBig : MonoBehaviour
{
    Controller controller;

    float x;
    float y;
    int shipID;
    bool touched;

    Transform shipBigPlace;
    float deltaX, deltaY;
    Vector2 mousePosition, initialPosition, standardPosition;
    private void Awake()
    {
        controller = FindObjectOfType<Controller>();
        shipID = controller.getShipID();
        touched = false;    
    }
    private void Start()
    {
        initialPosition = transform.position;
    }
    private void OnMouseDown()
    {
        if (!controller.isLocked())
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            controller.setID((shipID-1000), -1, false);
        }
    }
    private void OnMouseDrag()
    {
        if (!controller.isLocked())
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
    }
    private void OnMouseUp()
    {
        if (!controller.isLocked())
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
                    if (Mathf.Abs(vX-tX)<=0.18f && Mathf.Abs(vY-tY)<=0.18f)
                    {
                        transform.position = new Vector2(tX+2.4445f, tY-0.3745f);
                        controller.setID(shipID-1000, id,true);
                        if (touched)
                        {
                            controller.setID(shipID-1000, -1, false);
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
        Debug.Log("enter");
        touched = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        touched = false;
    }
}
